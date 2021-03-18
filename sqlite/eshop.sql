PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE category (
id INTEGER PRIMARY KEY,
name TEXT );
INSERT INTO category VALUES(1,'Giày lười nam');
CREATE TABLE seller_account (
id INTEGER PRIMARY KEY,
name TEXT,
phone_number TEXT,
password TEXT,
secret_key TEXT,
access_token TEXT,
access_expries TEXT,
refresh_token TEXT,
refresh_expries TEXT, email TEXT);
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
INSERT INTO sku VALUES(1,1,'giayVain',10,10,'den',45,20,20,20,20,2000.0999999999999089);
CREATE TABLE order_tbl (
id INTEGER PRIMARY KEY,
customer_id int,
create_date TEXT,
update_date TEXT,
payment_method TEXT,
shipping_fee INTEGER,
total_price INTEGER,
status TEXT,
FOREIGN KEY (customer_id) REFERENCES customer(id) );
CREATE TABLE order_item (
id INTEGER PRIMARY KEY,
order_id INTEGER,
sku_id INTEGER,
name TEXT,
variation TEXT,
item_price INTEGER,
quantity INTEGER, image TEXT,
FOREIGN KEY (order_id) REFERENCES order_tbl (id),
FOREIGN KEY (sku_id) REFERENCES sku (id) );
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
, secret_key TEXT);
CREATE TABLE brand (
id INTEGER PRIMARY KEY,
name TEXT
);
INSERT INTO brand VALUES(1,'No brand');
CREATE TABLE product (
 id INTEGER PRIMARY KEY, 
 category_id INTEGER,
 brand_id INTEGER,
 seller_id INTEGER,
 product_name TEXT, 
 description TEXT, 
 status TEXT, 
 FOREIGN KEY (category_id) REFERENCES category (id), 
 FOREIGN KEY (seller_id) REFERENCES seller_account (id),
 FOREIGN KEY (brand_id) REFERENCES brand (id) );
INSERT INTO product VALUES(1,1,1,1,'Giay vains','description','active');
CREATE TABLE image (
url TEXT PRIMARY KEY,
sku_id INTEGER,
FOREIGN KEY (sku_id) REFERENCES sku (id)
);
CREATE TABLE IF NOT EXISTS "cart" (
	"customer_id"	INTEGER,
	"shipping_fee"	INTEGER,
	"total_price"	INTEGER,
	"order_items"	TEXT,
	PRIMARY KEY("customer_id"),
	FOREIGN KEY (customer_id) REFERENCES customer (id)
);
CREATE TABLE address (
  id INTEGER PRIMARY KEY,
  customer_id INTEGER,
  street TEXT,
  address1 TEXT,
  address2 TEXT,
  address3 TEXT,
  FOREIGN KEY (customer_id) REFERENCES customer(id)
);
COMMIT;
