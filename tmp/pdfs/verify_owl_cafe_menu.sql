SELECT COUNT(*) AS imported_pdf_items
FROM "MenuItems"
WHERE "Description" LIKE 'Imported from Owl Cafe Menu PDF%';

SELECT c."Name" AS category, COUNT(*) AS item_count
FROM "MenuItems" m
JOIN "Categories" c ON c."Id" = m."CategoryId"
WHERE m."Description" LIKE 'Imported from Owl Cafe Menu PDF%'
GROUP BY c."Name"
ORDER BY c."Name";

SELECT c."Name" AS category, m."Name" AS item, m."Price" AS price
FROM "MenuItems" m
JOIN "Categories" c ON c."Id" = m."CategoryId"
WHERE (c."Name", m."Name") IN (
    ('Chinese', 'Lemon Paneer'),
    ('Chinese', 'Veg Manchurian Dry'),
    ('Chinese', 'Veg Manchurian Gravy'),
    ('Indian', 'Chicken Biryani'),
    ('Pizza', 'Chicken Chilli Pizza'),
    ('Beverages', 'Tea')
)
ORDER BY c."Name", m."Name";
