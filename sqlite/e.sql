PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE category (
id INTEGER PRIMARY KEY,
name TEXT );
CREATE TABLE seller_account (
id INTEGER PRIMARY KEY,
name TEXT,
phone_number TEXT,
password TEXT,
secret_key TEXT,
access_token TEXT,
access_expries TEXT,
refresh_token TEXT,
refresh_expries TEXT);
CREATE TABLE product (
id INTEGER PRIMARY KEY,
category_id INTEGER,
seller_id INTEGER,
product_name TEXT,
description TEXT,
status TEXT,
brand TEXT,
FOREIGN KEY (category_id) REFERENCES category (id),
FOREIGN KEY (seller_id) REFERENCES seller_account (id) );
CREATE TABLE sku (
id INTEGER PRIMARY KEY,
product_id INTEGER,
seller_sku TEXT,
available INTEGER,
quantity INTEGER,
color TEXT,
size INTEGER,
height INTEGER,
width INTEGER,
length INTEGER,
weight INTEGER,
price INTEGER,
FOREIGN KEY (product_id) REFERENCES product (id)
);
CREATE TABLE order_tbl (
id INTEGER PRIMARY KEY,
create_date TEXT,
update_date TEXT,
payment_method TEXT,
shipping_fee INTEGER,
total_price INTEGER,
status TEXT , customer_id);
CREATE TABLE order_item (
id INTEGER PRIMARY KEY,
order_id INTEGER,
sku_id INTEGER,
name TEXT,
variation TEXT,
item_price INTEGER,
quantity INTEGER,
FOREIGN KEY (order_id) REFERENCES order_tbl (id),
FOREIGN KEY (sku_id) REFERENCES sku (id) );
CREATE TABLE image (
id INTEGER PRIMARY KEY,
sku_id INTEGER,
image TEXT,
FOREIGN KEY (sku_id) REFERENCES sku (id)
);
CREATE TABLE customer (
id INTEGER PRIMARY KEY,
name TEXT,
email TEXT,
phone_number TEXT,
gender TEXT,
password TEXT,
access_token TEXT,
access_expries TEXT,
refresh_token TEXT,
refresh_expries TEXT
);
CREATE TABLE brand(
id INTEGER PRIMARY KEY,
name TEXT
);
COMMIT;
