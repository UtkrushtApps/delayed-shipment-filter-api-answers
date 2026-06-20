# Solution Steps

1. Update the delayed shipments service so the database query applies the actual business filter: exclude shipments whose status is Delivered and only include shipments whose estimated delivery date is at least minDays days in the past.

2. Compute a cutoff date with DateTime.Now.AddDays(-minDays), then filter with EstimatedDeliveryDate <= cutoffDate.

3. Project directly to ShipmentDto and use ToListAsync so an empty result naturally returns an empty list instead of throwing.

4. Remove unsafe collection access such as First(), because no matching shipments is a valid outcome.

5. Change the controller action to accept the raw minDays query value as a nullable string instead of relying on automatic integer model binding.

6. Default minDays to 1 only when the query parameter is omitted.

7. Validate the provided minDays value with int.TryParse and reject zero, negative, empty, non-numeric, or overflow values with HTTP 400 and a clear message.

8. Call the service with the parsed positive integer and return HTTP 200 with the resulting list.

