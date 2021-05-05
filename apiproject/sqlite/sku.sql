PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);
INSERT INTO AspNetRoles VALUES('3c6ced2c-cab9-4d39-8e88-82784a334717','customer','CUSTOMER','a8ea2ef8-8869-4876-8c44-7d95d3cc9c22');
INSERT INTO AspNetRoles VALUES('24ba40a8-1b4a-48ad-8162-73c52bfadd1b','Seller','SELLER','5c4aaa28-e956-4b95-819a-bafdc094b63a');
CREATE TABLE IF NOT EXISTS "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "brands" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_brands" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NULL
);
INSERT INTO brands VALUES(1,'ADIDAS');
INSERT INTO brands VALUES(2,'CALVIN KLEIN');
INSERT INTO brands VALUES(3,'CROCS');
INSERT INTO brands VALUES(4,'NIKE');
INSERT INTO brands VALUES(5,'VANS');
INSERT INTO brands VALUES(6,'CONVERSE');
CREATE TABLE IF NOT EXISTS "category" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_category" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NULL
);
INSERT INTO category VALUES(1,'Boot');
INSERT INTO category VALUES(2,'Lifestyle');
INSERT INTO category VALUES(3,'Running');
INSERT INTO category VALUES(4,'Basketball');
INSERT INTO category VALUES(5,'Football');
INSERT INTO category VALUES(6,'Training & Gym');
INSERT INTO category VALUES(7,'Golf');
INSERT INTO category VALUES(8,'Athletics');
INSERT INTO category VALUES(9,'Walking');
CREATE TABLE IF NOT EXISTS "customers" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_customers" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NULL,
    "email" TEXT NULL,
    "phoneNumber" TEXT NULL,
    "password" TEXT NULL,
    "accessToken" TEXT NULL,
    "accesExpire" TEXT NOT NULL,
    "refreshToken" TEXT NULL
, "gender" TEXT default 'male');
CREATE TABLE IF NOT EXISTS "ordertable" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_ordertable" PRIMARY KEY AUTOINCREMENT,
    "createDate" TEXT NOT NULL,
    "updateDate" TEXT NOT NULL,
    "paymentMethod" TEXT NULL,
    "shippingFee" TEXT NOT NULL,
    "shippingAddress" TEXT NULL,
    "totalPrice" TEXT NOT NULL,
    "status" TEXT NULL

	, "customerId" INTEGER REFERENCES "customers" ("id")
);
INSERT INTO ordertable VALUES(1,'2021-04-21 00:00:00','2021-04-21 00:00:00','COD','2000.0','23 Nguyen Chi Thanh','400000.0','pending',1);
INSERT INTO ordertable VALUES(2,'2021-04-21T00:00:00','2021-04-21T00:00:00','COD','2000.0','23 Nguyen Chi Thanh','400000.0','pending',1);
INSERT INTO ordertable VALUES(3,'2021-04-21T00:00:00','2021-04-21T00:00:00','COD','2000.0','23 Nguyen Chi Thanh','400000.0','canceled',1);
INSERT INTO ordertable VALUES(4,'2021-04-20 00:00:00','2021-04-20 00:00:00','VISA','2000.0','23 Nguyen Chi Thanh','300000.0','pending',1);
INSERT INTO ordertable VALUES(5,'2021-04-19 00:00:00','2021-04-19 00:00:00','VISA','2000.0','23 Nguyen Chi Thanh','300000.0','rts',1);
INSERT INTO ordertable VALUES(6,'2021-04-19 00:00:00','2021-04-19 00:00:00','VISA','2000.0','23 Nguyen Chi Thanh','300000.0','rts',1);
CREATE TABLE IF NOT EXISTS "refreshtokens" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_refreshtokens" PRIMARY KEY AUTOINCREMENT,
    "userName" TEXT NULL,
    "refreshToken" TEXT NULL,
    "isrevoked" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "seller" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_seller" PRIMARY KEY AUTOINCREMENT,
    "phoneNumer" TEXT NULL,
    "name" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "password" TEXT NOT NULL,
    "secretKey" TEXT NULL,
    "accessToken" TEXT NULL,
    "accessExpire" TEXT NOT NULL,
    "refreshToken" TEXT NULL
, phoneNumber TEXT);
CREATE TABLE IF NOT EXISTS "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "product" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product" PRIMARY KEY AUTOINCREMENT,
    "categoryId" INTEGER NOT NULL,
    "brandId" INTEGER NOT NULL,
    "productName" TEXT NULL,
    "description" TEXT NULL,
    "status" TEXT NULL, sellerId INTEGER REFERENCES seller(id),

    CONSTRAINT "FK_product_brands_brandId" FOREIGN KEY ("brandId") REFERENCES "brands" ("id") ON DELETE CASCADE,

    CONSTRAINT "FK_product_category_categoryId" FOREIGN KEY ("categoryId") REFERENCES "category" ("id") ON DELETE CASCADE
);
INSERT INTO product VALUES(1,1,1,'New phone','<p>Available 4/23 at 9:00 AM</p><p>After making waves as a pioneering basketball sneaker, the Air Force 1 morphed from a hardwood pioneer into a beloved streetwear mainstay. The silhouette continues to stand tall in the face of constantly shifting cultural trends—and the latest edition arrives wrapped in messaging speaking to the style''s enduring appeal.</p>','active',1);
INSERT INTO product VALUES(2,6,4,'Air Force 1','<p>Đường may tỉ mỉ chắc chắn, dễ vệ sinh<br>Thiết kế đặc trưng mang đậm chất đường phố<br>Dễ dàng phối đồ trong mọi hoàn cảnh<br>Đế giày có độ bám cao</p><p>Kiểu dáng cổ điển</p><p><strong>Giày Sneaker Unisex Old Skool Vans VN000D3HY28 - Black/White</strong> là dòng giày được thiết kế đa dạng kiểu dáng bên thân giày. Kiểu dáng cổ điển, dễ vệ sinh, với đường may tỉ mỉ chắc chắn.</p>','active',1);
INSERT INTO product VALUES(3,1,1,'Nike Air Max 96 II','<p><strong>96 TAKE 2, IT''S A NO-BRAINER.</strong></p><p><br>&nbsp;</p><p>For the first time, the Air Max 96 II returns with a 1–1 remake. Bringing back the sporty aesthetic imagined by famed Nike designer Sergio Lorenzo, with retro colours on a classic ''90s construction, it''s a return to throwback athletics styling. Nike Air cushioning keeps it comfortable. Long live the bubble.</p><p><br>&nbsp;</p><p><strong>Benefits</strong></p><ul><li>Originally designed for sport, the Air cushioning in the heel and forefoot add unparalleled comfort.</li><li>The fused and stitched overlays add heritage styling and durability while creating a sporty aesthetic.</li><li>The low-cut, padded collar looks sleek and athletic while feeling great.</li><li>Original colours create a throwback vibe and combine with a retro branding on the tongue.</li></ul><p><br>&nbsp;</p><p><strong>Product Details</strong></p><ul><li>Embroidered Swoosh design</li><li>Rubber outsole</li><li>Not intended for use as Personal Protective Equipment (PPE)</li><li>Colour Shown: White/Midnight Navy/University Gold/Black</li><li>Style: CZ1921-100</li><li>Country/Region of Origin: Vietnam</li></ul><p><br>&nbsp;</p><p><strong>Nike Air Max Origins</strong></p><p>Revolutionary Air technology first made its way into Nike footwear in 1978. In 1987, the Air Max 1 debuted with visible Air technology in its heel, allowing fans more than just the feel of Air cushioning—suddenly they could see it. Since then, next-generation Air Max shoes have become a hit with athletes and collectors by offering striking colour combinations and reliable, lightweight cushioning.</p>','active',2);
CREATE TABLE IF NOT EXISTS "address" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_addresses" PRIMARY KEY AUTOINCREMENT,
    "customerId" INTEGER NOT NULL,
    "street" TEXT NULL,
    "address1" TEXT NULL,
    "address2" TEXT NULL,
    "address3" TEXT NULL,
    
    "isDefault" INTEGER DEFAULT 0,

	FOREIGN KEY ("customerId") REFERENCES "customers" ("id")
);
INSERT INTO address VALUES(1,1,'46 An Duong Vuong','Thành phố Hồ Chí Minh','Quận 5','Phường 05',0);
CREATE TABLE IF NOT EXISTS "cart" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_cart" PRIMARY KEY AUTOINCREMENT,
    "customerId" INTEGER NULL,
    "shippingFee" TEXT NOT NULL,
    "totalPrice" TEXT NOT NULL,
 	 "items" TEXT NULL
);
INSERT INTO cart VALUES(1,1,'0.0','0.0','[]');
CREATE TABLE IF NOT EXISTS "selleraddresses" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_selleraddresses" PRIMARY KEY AUTOINCREMENT,
    "sellerId" INTEGER NOT NULL,
    "street" TEXT NULL,
    "address1" TEXT NULL,
    "address2" TEXT NULL,
    "address3" TEXT NULL, isDefault INTEGER,

 FOREIGN KEY ("sellerId") REFERENCES "seller" ("id")
);
INSERT INTO selleraddresses VALUES(1,1,'23 Nguyen Trai','Thành phố Hồ Chí Minh','Quận 1','Phường Tân Định',1);
INSERT INTO selleraddresses VALUES(2,1,'245 Nguyen Gia Tri','Thành phố Hồ Chí Minh','Quận Bình Thạnh','Phường 05',0);
INSERT INTO selleraddresses VALUES(4,1,'67 Nguyễn Chí Thanh','Tỉnh Bình Dương','Thành phố Thủ Dầu Một','Phường Phú Mỹ',0);
CREATE TABLE IF NOT EXISTS "skus" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_skus" PRIMARY KEY AUTOINCREMENT,
    "seller_sku" TEXT NULL,
    "productId" INTEGER NOT NULL,
    "available" INTEGER NOT NULL,
    "quantity" INTEGER NOT NULL,
    "color" TEXT NULL,
    "size" INTEGER NOT NULL,
    "height" INTEGER NOT NULL,
    "width" INTEGER NOT NULL,
    "length" INTEGER NOT NULL,
    "weight" INTEGER NOT NULL,
    "price" TEXT NOT NULL, sellerSku,

 FOREIGN KEY ("productId") REFERENCES "product" ("id")
);
INSERT INTO skus VALUES(1,NULL,2,10,10,'White',40,60,30,20,2,'34000000.0','sku test');
INSERT INTO skus VALUES(2,NULL,1,10,10,'Red',30,40,300,200,30,'35.0','sku test');
INSERT INTO skus VALUES(21,NULL,1,3,3,'Black',43,40,300,200,30,'4999000.0','sku test');
INSERT INTO skus VALUES(22,NULL,3,0,20,'White',30,10,10,10,1,'4699000.0','sku test');
INSERT INTO skus VALUES(23,NULL,3,0,10,'Black',30,10,10,10,1,'4699000.0','sku test');
INSERT INTO skus VALUES(24,NULL,2,30,30,'Gray',40,60,30,20,2,'34000001.0','sku gray');
CREATE TABLE IF NOT EXISTS "images" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_images" PRIMARY KEY AUTOINCREMENT,
    "url" TEXT NULL,
    "skuId" INTEGER NOT NULL,

CONSTRAINT "FK_images_skus_skuId" FOREIGN KEY ("skuId") REFERENCES "skus" ("id") ON DELETE CASCADE
);
INSERT INTO images VALUES(8,'api/images/bitmap/625839531132634630866928823.jpg',1);
INSERT INTO images VALUES(9,'api/images/bitmap/788583574132634642833463767.jpg',2);
INSERT INTO images VALUES(10,'api/images/bitmap/1239753696132634644973906514.jpg',21);
INSERT INTO images VALUES(11,'api/images/bitmap/378192762132634644973921836.jpg',21);
INSERT INTO images VALUES(12,'api/images/bitmap/1750004175132634663836500523.jpg',22);
INSERT INTO images VALUES(13,'api/images/bitmap/366525669132634663836671363.jpg',22);
INSERT INTO images VALUES(14,'api/images/bitmap/1146135653132634663836876432.jpg',23);
INSERT INTO images VALUES(15,'api/images/bitmap/1054626825132634663836898006.jpg',23);
INSERT INTO images VALUES(16,'api/images/bitmap/188410644132635826037565869.jpg',24);
CREATE TABLE IF NOT EXISTS "orderitem" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_orderitem" PRIMARY KEY AUTOINCREMENT,
    "orderId" INTEGER NOT NULL,
    "skuId" INTEGER NOT NULL,
    "name" TEXT NULL,
    "variation" TEXT NULL,
    "price" TEXT NOT NULL,
    "quantity" INTEGER NOT NULL,

FOREIGN KEY ("orderId") REFERENCES "ordertable" ("id"),

FOREIGN KEY ("skuId") REFERENCES "skus" ("id")
);
INSERT INTO orderitem VALUES(1,1,1,'Nike Air Max 96 II','Black 40','400000.0',1);
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('brands',6);
INSERT INTO sqlite_sequence VALUES('category',9);
INSERT INTO sqlite_sequence VALUES('product',3);
INSERT INTO sqlite_sequence VALUES('skus',24);
INSERT INTO sqlite_sequence VALUES('refreshtokens',43);
INSERT INTO sqlite_sequence VALUES('customers',25);
INSERT INTO sqlite_sequence VALUES('images',16);
INSERT INTO sqlite_sequence VALUES('ordertable',6);
INSERT INTO sqlite_sequence VALUES('orderitem',1);
INSERT INTO sqlite_sequence VALUES('seller',2);
INSERT INTO sqlite_sequence VALUES('selleraddresses',4);
INSERT INTO sqlite_sequence VALUES('cart',1);
INSERT INTO sqlite_sequence VALUES('address',1);
CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");
CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");
CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");
CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");
COMMIT;
