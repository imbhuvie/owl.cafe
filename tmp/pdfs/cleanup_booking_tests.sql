DELETE FROM "Bookings"
WHERE "Email" LIKE 'bookingtest%@example.com';

SELECT COUNT(*) AS remaining_automated_booking_tests
FROM "Bookings"
WHERE "Email" LIKE 'bookingtest%@example.com';
