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
INSERT INTO AspNetUsers VALUES('478025c7-bd5f-46c3-8948-08631dc3c75a','0858267296','0858267296','tranphanthanhlong18@gmail.com','TRANPHANTHANHLONG18@GMAIL.COM',0,'AQAAAAEAACcQAAAAENZy/Dm7BcWHkoxukn9KnLt+rnH1+Vv5ff+HbMUPhIf46LuERUG7r3Y8JvS/5vlGXA==','JJHREV2XUSLEWVR4EWCMWU2JOYVC2N5A','e7ec914d-980e-4b2a-a421-29229cde029a','tiktzuki',1,0,NULL,1,0);
INSERT INTO AspNetUsers VALUES('247e5489-1d75-482a-befd-22366f924b2d','0858267297','0858267297','long@gmail.com','LONG@GMAIL.COM',0,'AQAAAAEAACcQAAAAEN5QRUCZxSC1fRNu4oIjVWXff6fXo2Yv90HBLeeTtl+AgaVbIMkeN/sx51sIeCs2fA==','I4KE76XL37WQ2ARJHF446U67QPT4LTFN','d9fb3f6c-f93f-43e8-a248-88da8c913bd7','Long Tran',0,0,NULL,1,0);
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
INSERT INTO customers VALUES(2,'long','tranphanthanhlong18@gmail.com','0858267296','AQAAAAEAACcQAAAAENZy/Dm7BcWHkoxukn9KnLt+rnH1+Vv5ff+HbMUPhIf46LuERUG7r3Y8JvS/5vlGXA==',NULL,'0001-01-01 00:00:00',NULL,'male');
CREATE TABLE IF NOT EXISTS "paymentintent"(
  "PaymentIntentId" TEXT NOT NULL PRIMARY KEY,
  "ClientSecret" TEXT
);
INSERT INTO paymentintent VALUES('pi_1IpnHwB8Uv4yEiL65VTOjLrw','pi_1IpnHwB8Uv4yEiL65VTOjLrw_secret_gPATl33cQOrBucQC1HhB3z34w');
INSERT INTO paymentintent VALUES('pi_1IppcEB8Uv4yEiL6X1QAcAWu','pi_1IppcEB8Uv4yEiL6X1QAcAWu_secret_7DWYRKS2opGgllvMv61uiD1hF');
CREATE TABLE IF NOT EXISTS "ordertable" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_ordertable" PRIMARY KEY AUTOINCREMENT,
    "createDate" TEXT NOT NULL,
    "updateDate" TEXT NOT NULL,
    "paymentMethod" TEXT NULL,
    "shippingFee" TEXT NOT NULL,
    "shippingAddress" TEXT NULL,
    "totalPrice" TEXT NOT NULL,
    "status" TEXT NULL,
    "PaymentIntentID" TEXT REFERENCES "paymentintent"("PaymentIntentId"),
    "customerId" INTEGER REFERENCES "customers" ("id")
);
INSERT INTO ordertable VALUES(1,'2021-05-11 00:00:00','2021-05-11 00:00:00','VISA','22.0','40 Nguyen Van Cu, Thành phố Hồ Chí Minh, Quận 5, Phường 04','259.0','canceled','pi_1IpnHwB8Uv4yEiL65VTOjLrw',2);
INSERT INTO ordertable VALUES(2,'2021-05-11 00:00:00','2021-05-11 00:00:00','PAYPAL','22.0','40 Nguyen Van Cu, Thành phố Hồ Chí Minh, Quận 5, Phường 04','34000212.0','rts','pi_1IppcEB8Uv4yEiL6X1QAcAWu',2);
CREATE TABLE IF NOT EXISTS "refreshtokens" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_refreshtokens" PRIMARY KEY AUTOINCREMENT,
    "userName" TEXT NULL,
    "refreshToken" TEXT NULL,
    "isrevoked" INTEGER NOT NULL
);
INSERT INTO refreshtokens VALUES(1,'0858267296','9d389372-7141-4847-bb4f-d3ef7d3f696c',0);
INSERT INTO refreshtokens VALUES(2,'0858267296','56768ae5-81ad-47fd-b6b1-f71514eadde2',0);
INSERT INTO refreshtokens VALUES(3,'0858267296','4c787787-db5f-4edf-973b-0537cd717a86',0);
INSERT INTO refreshtokens VALUES(4,'0858267296','b0394cd1-e48b-470c-89ed-d4579c6b0c36',0);
INSERT INTO refreshtokens VALUES(5,'0858267297','8c5e04b0-a192-477e-acb9-0adf04119261',0);
INSERT INTO refreshtokens VALUES(6,'0858267297','c4e0721f-4842-440b-bd45-1c155c711c86',0);
INSERT INTO refreshtokens VALUES(7,'0858267296','b2f53034-3c56-4d90-9c19-413e99c0ee7c',0);
INSERT INTO refreshtokens VALUES(8,'0858267297','3891994c-3828-4436-a32e-6f36305f4347',0);
CREATE TABLE IF NOT EXISTS "seller" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_seller" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "password" TEXT NOT NULL,
    "secretKey" TEXT NULL,
    "accessToken" TEXT NULL,
    "accessExpire" TEXT NOT NULL,
    "refreshToken" TEXT NULL,
    "phoneNumber" TEXT
);
INSERT INTO seller VALUES(4,'Long Tran','long@gmail.com','AQAAAAEAACcQAAAAEN5QRUCZxSC1fRNu4oIjVWXff6fXo2Yv90HBLeeTtl+AgaVbIMkeN/sx51sIeCs2fA==',NULL,NULL,'0001-01-01 00:00:00',NULL,'0858267297');
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
INSERT INTO AspNetUserRoles VALUES('478025c7-bd5f-46c3-8948-08631dc3c75a','3c6ced2c-cab9-4d39-8e88-82784a334717');
INSERT INTO AspNetUserRoles VALUES('247e5489-1d75-482a-befd-22366f924b2d','24ba40a8-1b4a-48ad-8162-73c52bfadd1b');
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
    "status" TEXT NULL,
    "sellerId" INTEGER NOT NULL
);
INSERT INTO product VALUES(1,2,4,'Air More Uptempo ''96','<p>Available 4/23 at 9:00 AM</p><p>After making waves as a pioneering basketball sneaker, the Air Force 1 morphed from a hardwood pioneer into a beloved streetwear mainstay. The silhouette continues to stand tall in th<strong>MORE IS MORE.</strong></p><p><br>&nbsp;</p><p>More visible Air. More giant letters. And more history. The Air More Uptempo ''96 captures the spirit of its era like no other sneaker—and yet somehow remains timeless. A ground-breaking shoe of its time known for the most encased Air during launch, the paper-thin exterior celebrates the fanfare, wrapping each upper in the original ad campaign. Peel off the tissue for the big reveal: OG leather and an "Air" graphic that you can quite literally see through.</p><p><br>&nbsp;</p><p><strong>Historic Insole</strong></p><p>The insole provides continuous comfort and features the original ad campaign of the ground-breaking shoe.</p><p><br>&nbsp;</p><p><strong>All About Air</strong></p><p>The shoe lives up to its name with an Air-Sole unit that has a plush feel with every step.</p><p><br>&nbsp;</p><p><strong>More Benefits</strong></p><ul><li>Leather has a classic feel that''s durable and easy to clean.</li><li>Rubber outsole offers excellent traction.</li><li>Colour Shown: Black/Chile Red/Glacier Blue/White</li><li>Style: DJ4633-010</li><li>Country/Region of Origin: Vietnam</li></ul>','active',1);
INSERT INTO product VALUES(2,6,4,'Air Force 1','<p>Đường may tỉ mỉ chắc chắn, dễ vệ sinh<br>Thiết kế đặc trưng mang đậm chất đường phố<br>Dễ dàng phối đồ trong mọi hoàn cảnh<br>Đế giày có độ bám cao</p><p>Kiểu dáng cổ điển</p><p><strong>Giày Sneaker Unisex Old Skool Vans VN000D3HY28 - Black/White</strong> là dòng giày được thiết kế đa dạng kiểu dáng bên thân giày. Kiểu dáng cổ điển, dễ vệ sinh, với đường may tỉ mỉ chắc chắn.</p>','active',1);
INSERT INTO product VALUES(3,1,1,'Nike Air Max 96 II','<p><strong>96 TAKE 2, IT''S A NO-BRAINER.</strong></p><p><br>&nbsp;</p><p>For the first time, the Air Max 96 II returns with a 1–1 remake. Bringing back the sporty aesthetic imagined by famed Nike designer Sergio Lorenzo, with retro colours on a classic ''90s construction, it''s a return to throwback athletics styling. Nike Air cushioning keeps it comfortable. Long live the bubble.</p><p><br>&nbsp;</p><p><strong>Benefits</strong></p><ul><li>Originally designed for sport, the Air cushioning in the heel and forefoot add unparalleled comfort.</li><li>The fused and stitched overlays add heritage styling and durability while creating a sporty aesthetic.</li><li>The low-cut, padded collar looks sleek and athletic while feeling great.</li><li>Original colours create a throwback vibe and combine with a retro branding on the tongue.</li></ul><p><br>&nbsp;</p><p><strong>Product Details</strong></p><ul><li>Embroidered Swoosh design</li><li>Rubber outsole</li><li>Not intended for use as Personal Protective Equipment (PPE)</li><li>Colour Shown: White/Midnight Navy/University Gold/Black</li><li>Style: CZ1921-100</li><li>Country/Region of Origin: Vietnam</li></ul><p><br>&nbsp;</p><p><strong>Nike Air Max Origins</strong></p><p>Revolutionary Air technology first made its way into Nike footwear in 1978. In 1987, the Air Max 1 debuted with visible Air technology in its heel, allowing fans more than just the feel of Air cushioning—suddenly they could see it. Since then, next-generation Air Max shoes have become a hit with athletes and collectors by offering striking colour combinations and reliable, lightweight cushioning.</p>','active',1);
INSERT INTO product VALUES(4,2,5,'Nike Air Max 90 SE','<p><strong>INFUSED WITH NATURE.</strong></p><p><br>&nbsp;</p><p>Nothing as fly, nothing as comfortable, nothing as proven—the Nike Air Max 90 SE stays true to its OG roots while taking a fresh step forwards.Made from at least 20% recycled content by weight, it includes 9% cork in the iconic Waffle outsole, stitched overlays with 100% recycled denim and an embroidered plant graphic.Max Air cushioning adds comfort to your journey.</p><p><br>&nbsp;</p><p><strong>Tried and True Made New</strong></p><p>Bringing the old-school look of Nike running into a new realm, the upper is made from recycled canvas.Hits of cork in the Swoosh design and heel update your style.</p><p><br>&nbsp;</p><p><strong>Details that Speak</strong></p><p>The embroidered botanical illustration on the tongue nods to the plant dye used in the shoe.The natural colour palette celebrates nature and highlights the unique construction.</p><p><br>&nbsp;</p><p><strong>Heritage Style Refreshed</strong></p><p>The mixed cork-and-rubber Waffle outsole adds traction, durability and heritage style to a design that looks towards the future.</p><p><br>&nbsp;</p><p><strong>More Benefits</strong></p><ul><li>Originally designed for performance running, the Max Air unit in the heel adds unbelievable cushioning.</li><li>Low-top, padded collar looks sleek and feels great.</li><li>Stitched overlays and TPU accents add durability, comfort and the iconic ''90s look you love.</li><li>Volt-coloured insole feels extra-comfy and supportive.</li></ul><p><br>&nbsp;</p><p><strong>Product Details</strong></p><ul><li>Recycled EVA foam insole</li><li>Foam midsole</li><li>Recycled laces</li><li>Colour Shown: Obsidian/Coconut Milk/White/Wheat</li><li>Style: DD0385-400</li><li>Country/Region of Origin: Vietnam</li></ul><p><br>&nbsp;</p><p><strong>Nike Air Max Origins</strong></p><p>Revolutionary Air technology first made its way into Nike footwear in 1978.In 1987, the Air Max 1 debuted with visible Air technology in its heel, allowing fans more than just the feel of Air cushioning—suddenly they could see it.Since then, next-generation Air Max shoes have become a hit with athletes and collectors by offering striking colour combinations and reliable, lightweight cushioning.</p>','active',1);
INSERT INTO product VALUES(5,4,4,'Nike Cosmic Unity','<p><strong>INTRODUCING THE COSMIC UNITY.</strong></p><p><br>&nbsp;</p><p>The Nike Cosmic Unity is trash that helps keep you fresh, giving you back the energy you need to let your game do the talking when it counts. We built a sleek and comfortable silhouette with sustainability in mind. It''s made from at least 25% recycled material by weight and comes equipped with a partially recycled Air Zoom Strobel designed to help you be efficient off the dribble or effective on defence. This is trash, transformed into a tool for basketball players to get a little more out of their game, while taking a little less from the planet.</p><p><br>&nbsp;</p><p><strong>More Trash. Fresh Game.</strong></p><p>A sleek design, made from at least 25% recycled material by weight, features a full-length Air Zoom Strobel unit, which provides the energy return to help you feel fresh all game long.</p><p><br>&nbsp;</p><p><strong>Built for Efficiency</strong></p><p>This shoe is light, durable and made from partially recycled materials in the upper, midsole, lace system and outsole. It''s built to contain your every move, from smooth turnaround jumpers to slick lay-ups off the glass.</p><p><br>&nbsp;</p><p><strong>From the Floor to the Finish</strong></p><p>A thin, durable outsole, which uses rubber partially made from recycled materials, provides the on-court traction you need to take defenders off the dribble and change direction at will.</p>','active',0);
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
INSERT INTO address VALUES(2,2,'40 Nguyen Van Cu','Thành phố Hồ Chí Minh','Quận 5','Phường 04',1);
CREATE TABLE IF NOT EXISTS "cart" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_cart" PRIMARY KEY AUTOINCREMENT,
    "customerId" INTEGER NULL,
    "shippingFee" TEXT NOT NULL,
    "totalPrice" TEXT NOT NULL,
 	 "items" TEXT NULL
);
INSERT INTO cart VALUES(2,2,'0.0','0.0','[]');
CREATE TABLE IF NOT EXISTS "selleraddresses" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_selleraddresses" PRIMARY KEY AUTOINCREMENT,
    "sellerId" INTEGER NOT NULL,
    "street" TEXT NULL,
    "address1" TEXT NULL,
    "address2" TEXT NULL,
    "address3" TEXT NULL, isDefault INTEGER,
 FOREIGN KEY ("sellerId") REFERENCES "seller" ("id")
);
INSERT INTO selleraddresses VALUES(1,4,'865 Nguyen Van Linh','Thành phố Hồ Chí Minh','Quận 7','Phường Tân Phú',1);
CREATE TABLE IF NOT EXISTS "skus" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_skus" PRIMARY KEY AUTOINCREMENT,
    "productId" INTEGER NOT NULL,
    "available" INTEGER NOT NULL,
    "quantity" INTEGER NOT NULL,
    "color" TEXT NULL,
    "size" INTEGER NOT NULL,
    "height" INTEGER NOT NULL,
    "width" INTEGER NOT NULL,
    "length" INTEGER NOT NULL,
    "weight" INTEGER NOT NULL,
    "price" TEXT NOT NULL, 
    sellerSku TEXT NULL DEFAULT "seller sku",

 FOREIGN KEY ("productId") REFERENCES "product" ("id")
);
INSERT INTO skus VALUES(1,2,9,9,'White',40,60,30,20,2,'34000000.0','sku test');
INSERT INTO skus VALUES(2,1,8,8,'Red',30,40,300,200,30,'216.0','sku test');
INSERT INTO skus VALUES(3,1,3,3,'Black',43,40,300,200,30,'217.0','sku test');
INSERT INTO skus VALUES(4,3,0,20,'White',30,10,10,10,1,'4699000.0','sku test');
INSERT INTO skus VALUES(5,3,0,10,'Black',30,10,10,10,1,'4699000.0','sku test');
INSERT INTO skus VALUES(6,2,30,30,'Gray',40,60,30,20,2,'34000001.0','sku gray');
INSERT INTO skus VALUES(7,4,29,29,'White',40,40,40,40,1,'167.0','AirMax90SEWhite');
INSERT INTO skus VALUES(8,4,10,10,'Brown',40,40,40,40,1,'167.0','AirMax90SEBrow');
INSERT INTO skus VALUES(9,5,9,9,'Organe',40,30,30,30,1,'190.0','NikeCosmicUnity-orange');
INSERT INTO skus VALUES(10,5,10,10,'Black',41,30,30,30,1,'190.0','NikeCosmicUnity-black');
CREATE TABLE IF NOT EXISTS "images" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_images" PRIMARY KEY AUTOINCREMENT,
    "url" TEXT NULL,
    "skuId" INTEGER NOT NULL,
CONSTRAINT "FK_images_skus_skuId" FOREIGN KEY ("skuId") REFERENCES "skus" ("id") ON DELETE CASCADE
);
INSERT INTO images VALUES(1,'api/images/bitmap/625839531132634630866928823.jpg',1);
INSERT INTO images VALUES(2,'api/images/bitmap/788583574132634642833463767.jpg',2);
INSERT INTO images VALUES(3,'api/images/bitmap/1239753696132634644973906514.jpg',2);
INSERT INTO images VALUES(4,'api/images/bitmap/378192762132634644973921836.jpg',2);
INSERT INTO images VALUES(5,'api/images/bitmap/1750004175132634663836500523.jpg',3);
INSERT INTO images VALUES(6,'api/images/bitmap/366525669132634663836671363.jpg',3);
INSERT INTO images VALUES(7,'api/images/bitmap/1146135653132634663836876432.jpg',4);
INSERT INTO images VALUES(8,'api/images/bitmap/1054626825132634663836898006.jpg',5);
INSERT INTO images VALUES(9,'api/images/bitmap/188410644132635826037565869.jpg',6);
INSERT INTO images VALUES(10,'api/images/bitmap/2054017493132650424113979821.jpg',7);
INSERT INTO images VALUES(11,'api/images/bitmap/1258617576132650424114021128.jpg',7);
INSERT INTO images VALUES(12,'api/images/bitmap/580893077132650424113979821.jpg',7);
INSERT INTO images VALUES(13,'api/images/bitmap/587949909132650424116806967.jpg',8);
INSERT INTO images VALUES(14,'api/images/bitmap/26898852132650424116826300.jpg',8);
INSERT INTO images VALUES(15,'api/images/bitmap/472781568132650424117229602.jpg',8);
INSERT INTO images VALUES(16,'api/images/bitmap/868092459132651861149651765.jpg',10);
INSERT INTO images VALUES(17,'api/images/bitmap/699667566132651861149328121.jpg',10);
INSERT INTO images VALUES(18,'api/images/bitmap/2130779839132651861149328165.jpg',9);
INSERT INTO images VALUES(19,'api/images/bitmap/1727250371132651861150253894.jpg',10);
INSERT INTO images VALUES(20,'api/images/bitmap/575771103132651861149328061.jpg',9);
INSERT INTO images VALUES(21,'api/images/bitmap/855643855132651861149328148.jpg',9);
CREATE TABLE IF NOT EXISTS "orderitem" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_orderitem" PRIMARY KEY AUTOINCREMENT,
    "orderId" INTEGER NOT NULL,
    "skuId" INTEGER NOT NULL,
    "name" TEXT NULL,
    "variation" TEXT NULL,
    "price" TEXT NOT NULL,
    "quantity" INTEGER NOT NULL, image TEXT,
FOREIGN KEY ("orderId") REFERENCES "ordertable" ("id"),
FOREIGN KEY ("skuId") REFERENCES "skus" ("id")
);
INSERT INTO orderitem VALUES(1,1,2,'New phone','Red 30','35.0',2,'api/images/bitmap/788583574132634642833463767.jpg');
INSERT INTO orderitem VALUES(2,1,7,'Nike Air Max 90 SE','White 40','167.0',1,'api/images/bitmap/2054017493132650424113979821.jpg');
INSERT INTO orderitem VALUES(3,2,1,'Air Force 1','White 40','34000000.0',1,'api/images/bitmap/625839531132634630866928823.jpg');
INSERT INTO orderitem VALUES(4,2,9,'Nike Cosmic Unity','Organe 40','190.0',1,'api/images/bitmap/2130779839132651861149328165.jpg');
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('brands',6);
INSERT INTO sqlite_sequence VALUES('category',9);
INSERT INTO sqlite_sequence VALUES('product',5);
INSERT INTO sqlite_sequence VALUES('skus',10);
INSERT INTO sqlite_sequence VALUES('refreshtokens',8);
INSERT INTO sqlite_sequence VALUES('customers',2);
INSERT INTO sqlite_sequence VALUES('images',21);
INSERT INTO sqlite_sequence VALUES('ordertable',2);
INSERT INTO sqlite_sequence VALUES('orderitem',4);
INSERT INTO sqlite_sequence VALUES('seller',4);
INSERT INTO sqlite_sequence VALUES('selleraddresses',1);
INSERT INTO sqlite_sequence VALUES('cart',2);
INSERT INTO sqlite_sequence VALUES('address',6);
CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");
CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");
CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");
CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");
COMMIT;
