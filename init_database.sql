CREATE TABLE carriers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(120) NOT NULL
);

CREATE TABLE shipments (
    id SERIAL PRIMARY KEY,
    tracking_code VARCHAR(40) NOT NULL,
    destination VARCHAR(200) NOT NULL,
    status VARCHAR(20) NOT NULL,
    estimated_delivery_date TIMESTAMP NOT NULL,
    carrier_id INT REFERENCES carriers(id),
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

INSERT INTO carriers (name) VALUES
    ('BlueLine Freight'),
    ('CargoMax'),
    ('RoadRunner Haulage');

INSERT INTO shipments (tracking_code, destination, status, estimated_delivery_date, carrier_id, created_at) VALUES
    ('SWH-1001', 'Chicago, IL', 'InTransit', NOW() - INTERVAL '6 days', 1, NOW() - INTERVAL '12 days'),
    ('SWH-1002', 'Dallas, TX', 'InTransit', NOW() - INTERVAL '4 days', 2, NOW() - INTERVAL '10 days'),
    ('SWH-1003', 'Denver, CO', 'Delivered', NOW() - INTERVAL '8 days', 1, NOW() - INTERVAL '15 days'),
    ('SWH-1004', 'Seattle, WA', 'Pending', NOW() - INTERVAL '2 days', 3, NOW() - INTERVAL '5 days'),
    ('SWH-1005', 'Miami, FL', 'InTransit', NOW() + INTERVAL '1 day', 2, NOW() - INTERVAL '3 days'),
    ('SWH-1006', 'Boston, MA', 'Delivered', NOW() - INTERVAL '5 days', 1, NOW() - INTERVAL '11 days'),
    ('SWH-1007', 'Phoenix, AZ', 'InTransit', NOW() - INTERVAL '10 days', 3, NOW() - INTERVAL '18 days'),
    ('SWH-1008', 'Atlanta, GA', 'Pending', NOW() - INTERVAL '1 day', 2, NOW() - INTERVAL '4 days'),
    ('SWH-1009', 'Portland, OR', 'Delivered', NOW() - INTERVAL '7 days', 3, NOW() - INTERVAL '14 days'),
    ('SWH-1010', 'Houston, TX', 'InTransit', NOW() - INTERVAL '3 days', 1, NOW() - INTERVAL '9 days');
