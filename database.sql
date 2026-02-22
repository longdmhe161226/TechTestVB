-- =============================================
-- Theater Ticket System - Database Schema
-- PostgreSQL
-- =============================================

-- Create database (run separately if needed)
-- CREATE DATABASE theater_db;

-- Drop tables in reverse dependency order (uncomment to reset)
DROP TABLE IF EXISTS seat_assignments CASCADE;
DROP TABLE IF EXISTS bookings CASCADE;
DROP TABLE IF EXISTS seat_prices CASCADE;
DROP TABLE IF EXISTS performances CASCADE;

-- Performances table
CREATE TABLE IF NOT EXISTS performances (
    id                SERIAL PRIMARY KEY,
    show_name         VARCHAR(200) NOT NULL,
    start_time        TIMESTAMP NOT NULL,
    duration_minutes  INT NOT NULL CHECK (duration_minutes > 0),
    created_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Bookings table
CREATE TABLE IF NOT EXISTS bookings (
    id                SERIAL PRIMARY KEY,
    performance_id    INT NOT NULL REFERENCES performances(id) ON DELETE CASCADE,
    customer_name     VARCHAR(200) NOT NULL,
    seat_type         VARCHAR(20) NOT NULL CHECK (seat_type IN ('Normal', 'VIP', 'Double')),
    quantity          INT NOT NULL CHECK (quantity > 0),
    total_price       DECIMAL(12,2) NOT NULL,
    created_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Seat assignments table
CREATE TABLE IF NOT EXISTS seat_assignments (
    id                SERIAL PRIMARY KEY,
    booking_id        INT NOT NULL REFERENCES bookings(id) ON DELETE CASCADE,
    performance_id    INT NOT NULL REFERENCES performances(id) ON DELETE CASCADE,
    seat_row          CHAR(1) NOT NULL CHECK (seat_row BETWEEN 'A' AND 'J'),
    seat_col          INT NOT NULL CHECK (seat_col BETWEEN 1 AND 10),
    UNIQUE(performance_id, seat_row, seat_col)
);

-- Index for faster seat lookups per performance
CREATE INDEX IF NOT EXISTS idx_seat_assignments_performance 
    ON seat_assignments(performance_id);

-- Index for faster booking lookups per performance
CREATE INDEX IF NOT EXISTS idx_bookings_performance 
    ON bookings(performance_id);

-- Seat prices table (dynamic pricing)
CREATE TABLE IF NOT EXISTS seat_prices (
    id              SERIAL PRIMARY KEY,
    seat_type       VARCHAR(20) NOT NULL UNIQUE,
    price           DECIMAL(12,2) NOT NULL CHECK (price >= 0),
    description     VARCHAR(200),
    is_active       BOOLEAN DEFAULT TRUE,
    updated_at      TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Seed default seat prices
INSERT INTO seat_prices (seat_type, price, description) VALUES
    ('Normal', 100000, 'Ghế thường'),
    ('VIP', 200000, 'Ghế VIP'),
    ('Double', 350000, 'Ghế đôi')
ON CONFLICT (seat_type) DO NOTHING;
