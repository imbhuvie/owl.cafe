from pathlib import Path


items = []


def add(category, course, name, price, is_veg=True, has_image=True):
    items.append(
        {
            "category": category,
            "course": course,
            "name": name.title(),
            "price": price,
            "is_veg": is_veg,
            "has_image": has_image,
        }
    )


for name, price in [
    ("LEMON PANEER", 279),
    ("CHILLY PANEER", 289),
    ("FRENCH FRIES", 179),
    ("HONEY CHILLI POTATO", 269),
    ("CHILLY POTATO", 249),
    ("PERI PERI FRIES", 199),
    ("CHEESY FRIES", 269),
    ("CRISPY CORN", 299),
]:
    add("Chinese", "Veg Starter", name, price)
add("Chinese", "Veg Starter", "VEG MANCHURIAN DRY", 249)
add("Chinese", "Veg Starter", "VEG MANCHURIAN GRAVY", 269)

for name, price in [
    ("LEMON CHICKEN", 369),
    ("CHICKEN LOLLIPOP", 369),
    ("CHICKEN WINGS", 389),
    ("DRUMS OF HEAVEN", 399),
    ("EGG BHURJI", 149),
    ("BOILED EGG", 99),
    ("CHICKEN FRY", 369),
]:
    add("Chinese", "Non Veg Starter", name, price, False)
add("Chinese", "Non Veg Starter", "CHILLY CHICKEN DRY", 369, False)
add("Chinese", "Non Veg Starter", "CHILLY CHICKEN GRAVY", 399, False)

for name, price in [
    ("MANCHURIAN RICE", 369),
    ("PANEER CHILLI RICE", 399),
    ("MUSHROOM CHILLY RICE", 369),
    ("SCHEZWAN RICE", 299),
    ("CHILLY GARLIC RICE", 299),
]:
    add("Chinese", "Veg Main Course", name, price)

for name, price in [
    ("CHICKEN MANCHURIAN RICE", 399),
    ("CHILLY CHICKEN RICE", 399),
    ("EGG FRIED RICE", 249),
    ("CHICKEN CHILLI GARLIC RICE", 379),
]:
    add("Chinese", "Non Veg Main Course", name, price, False)

for name, price in [
    ("SALTED PEANUT", 149),
    ("MASALA PAPAD", 169),
    ("MASALA PEANUT", 179),
]:
    add("Indian", "Veg Starter", name, price)

for name, price in [
    ("PANEER DO PYAZA", 329),
    ("KADHAI PANEER", 349),
    ("PANEER LABABDAR", 329),
    ("HANDI PANEER", 349),
    ("AALOO ZEERA", 199),
    ("MIX VEG", 249),
    ("DAL TADKA", 199),
    ("DAL FRY", 229),
    ("MUSHROOM KADHAI", 329),
    ("DAL MAKHANI", 289),
]:
    add("Indian", "Veg Main Course", name, price)

for name, price in [
    ("CHICKEN DO PYAZA", 389),
    ("KADHAI CHICKEN", 399),
    ("CHICKEN MASALA", 349),
    ("CHICKEN HANDI", 369),
    ("CHICKEN CHANGEZI", 349),
]:
    add("Indian", "Non Veg Main Course", name, price, False)

for name, price, is_veg in [
    ("STEAM RICE", 169, True),
    ("ZEERA RICE", 189, True),
    ("VEG BIRYANI", 269, True),
    ("VEG PULAO", 229, True),
    ("CHICKEN BIRYANI", 379, False),
    ("CHICKEN TIKKA BIRYANI", 399, False),
    ("CHICKEN PULAO", 329, False),
]:
    add("Indian", "Rice", name, price, is_veg)

for name, price in [
    ("TAWA ROTI", 29),
    ("BUTTER TAWA ROTI", 39),
    ("LACHCHA PARATHA", 79),
]:
    add("Indian", "Breads", name, price)

for name, price in [
    ("WHITE SAUCE PASTA", 349),
    ("RED SAUCE PASTA", 369),
    ("PINK SAUCE PASTA", 379),
    ("MAGGIE", 149),
    ("OWL PAHADI MAGGI", 169),
    ("GARLIC BREAD", 199),
    ("CHEESE GARLIC BREAD", 249),
]:
    add("Italian", "Italian", name, price)

for name, price in [("VANILLA ICE CREAM", 149), ("CHOCOLATE", 169)]:
    add("Desserts", "Dessert", name, price)

for name, price in [("GREEN SALAD", 99), ("DAHI KACHUMBER", 169)]:
    add("Salad", "Salad", name, price)

for name, price, is_veg in [
    ("MARGHERITA", 299, True),
    ("FARMHOUSE", 349, True),
    ("PANEER TIKKA", 349, True),
    ("CHICKEN TIKKA", 379, False),
    ("SWEET CORN PIZZA", 349, True),
    ("COUNTRY SPECIAL", 329, True),
    ("CHICKEN MARGHERITA", 399, False),
    ("CHICKEN CHILLI PIZZA", 399, False),
]:
    add("Pizza", "Pizza", name, price, is_veg)

# Page 5 has beverages but no beverage photos in the PDF.
for name, price in [
    ("MINT MOJITO", 199),
    ("BLUE LAGOON", 199),
    ("WATERMELON", 199),
    ("BLACK CURRANT", 199),
    ("VIRGIN MOJITO", 199),
    ("BLOODY MARRY", 225),
    ("FRESH LIME SODA", 179),
    ("AMERICAN NEWZEALAND", 279),
]:
    add("Beverages", "Mojitos", name, price, True, False)

for name, price in [
    ("BLUEBERRY", 289),
    ("STRAWBERRY", 289),
    ("OREO", 299),
    ("KITKAT", 299),
    ("CHOCOLATE", 279),
    ("COLD COFFEE", 299),
    ("MANGO", 279),
]:
    add("Beverages", "Shakes", name, price, True, False)

for name, price in [
    ("COFFEE", 179),
    ("TEA", 69),
    ("HOT CHOCOLATE", 249),
    ("BLACK COFFEE", 99),
]:
    add("Beverages", "Hot Beverages", name, price, True, False)

for name, price in [("RED BULL", 249), ("HELL", 110)]:
    add("Beverages", "Drinks", name, price, True, False)

# Deduplicate by category/name, preserving the first PDF occurrence.
seen = set()
deduped_items = []
for item in items:
    key = (item["category"].lower(), item["name"].lower())
    if key in seen:
        continue
    seen.add(key)
    deduped_items.append(item)
items = deduped_items

categories = [
    ("Chinese", "Authentic Chinese cuisine from the Owl Cafe PDF menu", 1),
    ("Indian", "Indian dishes from the Owl Cafe PDF menu", 2),
    ("Italian", "Italian dishes from the Owl Cafe PDF menu", 3),
    ("Pizza", "Pizzas from the Owl Cafe PDF menu", 4),
    ("Desserts", "Desserts from the Owl Cafe PDF menu", 5),
    ("Beverages", "Beverages from the Owl Cafe PDF menu", 6),
    ("Salad", "Salads from the Owl Cafe PDF menu", 7),
]

image_path = "/images/menu/owl-cafe-menu-dish.webp"


def sql_quote(value):
    return "'" + str(value).replace("'", "''") + "'"


lines = ["BEGIN;"]

for name, description, display_order in categories:
    lines.append(
        'INSERT INTO "Categories" ("Name", "Description", "Image", "DisplayOrder", "Status", "CreatedDate", "UpdatedDate") '
        f"SELECT {sql_quote(name)}, {sql_quote(description)}, NULL, {display_order}, TRUE, NOW(), NOW() "
        f'WHERE NOT EXISTS (SELECT 1 FROM "Categories" WHERE LOWER("Name") = LOWER({sql_quote(name)}));'
    )

lines.append(
    "WITH imported (category_name, item_name, description, price, is_veg, display_order, image_path) AS ("
)
values = []
for display_order, item in enumerate(items, 1):
    description = f"Imported from Owl Cafe Menu PDF - {item['course']}"
    image_value = sql_quote(image_path) if item["has_image"] else "NULL"
    values.append(
        "    ("
        f"{sql_quote(item['category'])}, "
        f"{sql_quote(item['name'])}, "
        f"{sql_quote(description)}, "
        f"{item['price']}, "
        f"{str(item['is_veg']).upper()}, "
        f"{display_order}, "
        f"{image_value}"
        ")"
    )

lines.append("VALUES\n" + ",\n".join(values) + "\n), matched AS (")
lines.append(
    '    SELECT i.*, c."Id" AS category_id '
    'FROM imported i JOIN "Categories" c ON LOWER(c."Name") = LOWER(i.category_name)'
)
lines.append("), updated AS (")
lines.append(
    '    UPDATE "MenuItems" m '
    'SET "Description" = matched.description, "Price" = matched.price, "DiscountPrice" = NULL, '
    '"Image" = matched.image_path, "IsVeg" = matched.is_veg, "IsAvailable" = TRUE, '
    '"IsFeatured" = FALSE, "PreparationTime" = 0, "Status" = TRUE, '
    '"DisplayOrder" = matched.display_order, "UpdatedDate" = NOW() '
    "FROM matched "
    'WHERE m."CategoryId" = matched.category_id AND LOWER(m."Name") = LOWER(matched.item_name) '
    'RETURNING m."Id"'
)
lines.append(")")
lines.append(
    'INSERT INTO "MenuItems" ("CategoryId", "Name", "Description", "Price", "DiscountPrice", "Image", '
    '"IsVeg", "IsAvailable", "IsFeatured", "PreparationTime", "Status", "DisplayOrder", "CreatedDate", "UpdatedDate") '
    "SELECT matched.category_id, matched.item_name, matched.description, matched.price, NULL, "
    "matched.image_path, matched.is_veg, TRUE, FALSE, 0, TRUE, matched.display_order, NOW(), NOW() "
    "FROM matched "
    'WHERE NOT EXISTS (SELECT 1 FROM "MenuItems" m WHERE m."CategoryId" = matched.category_id '
    "AND LOWER(m.\"Name\") = LOWER(matched.item_name));"
)
lines.append("COMMIT;")

output = Path("tmp/pdfs/import_owl_cafe_menu.sql")
output.write_text("\n".join(lines), encoding="utf-8")
print(f"items {len(items)}")
print(f"sql {output}")
