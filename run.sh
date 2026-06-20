#!/usr/bin/env bash
set -euo pipefail

cd /root/task

echo "==> Starting SwiftHaul Logistics services..."
docker compose up -d --build

echo "==> Waiting for database to become healthy..."
DB_READY=false
for i in $(seq 1 30); do
  if docker compose exec -T db pg_isready -U swifthaul -d swifthaul >/dev/null 2>&1; then
    DB_READY=true
    echo "    Database is ready."
    break
  fi
  echo "    ...waiting for database ($i)"
  sleep 3
done

if [ "$DB_READY" != "true" ]; then
  echo "!! Database did not become healthy. Showing logs:"
  docker compose logs db
  exit 1
fi

echo "==> Waiting for API to respond on health endpoint..."
API_READY=false
for i in $(seq 1 30); do
  if curl -sf http://127.0.0.1:8080/health >/dev/null 2>&1; then
    API_READY=true
    echo "    API is responding."
    break
  fi
  echo "    ...waiting for API ($i)"
  sleep 3
done

if [ "$API_READY" != "true" ]; then
  echo "!! API did not become healthy. Showing logs:"
  docker compose logs api
  exit 1
fi

echo ""
echo "============================================"
echo " Deployment successful!"
echo " API base URL: http://127.0.0.1:8080"
echo " Try: http://127.0.0.1:8080/api/shipments/delayed?minDays=3"
echo "============================================"
