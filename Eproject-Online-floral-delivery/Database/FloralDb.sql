CREATE DATABASE Eproject_Floral
go

USE Eproject_Floral
go

--ROLE
CREATE TABLE tbl_roles
(
roleID bigint not null primary key identity(1,1),
name nvarchar(100),
code varchar(100)
)
go

--CATEGORY
CREATE TABLE tbl_category
(
categoryID bigint not null primary key identity(1,1),
name nvarchar(150),
code varchar(150),
parentID int,
isActive bit,
isDelete bit
)
go

--MESSAGE CATEGORY
CREATE TABLE tbl_messageCategory
(
messageCategoryID bigint not null primary key identity(1,1),
name nvarchar(150),
code varchar(150),
isActive bit,
isDelete bit
)
go

--SHIPPING TYPE
CREATE TABLE tbl_shippingType
(
shippingTypeID bigint not null primary key identity(1,1),
name nvarchar(150),
code varchar(100),
isActive bit
)
go

--PAYMENT TYPE
CREATE TABLE tbl_paymentType
(
paymentTypeID bigint not null primary key identity(1,1),
name nvarchar(150),
code varchar(100),
isActive bit
)
go

--CITY
CREATE TABLE tbl_city
(
cityID bigint not null primary key identity(1,1),
name nvarchar(150),
code varchar(150)
)
go

--MESSAGE
CREATE TABLE tbl_message
(
messageID bigint not null primary key identity(1,1),
name nvarchar(150),
messageCategoryID bigint not null,
message nvarchar(700),
isActive bit

CONSTRAINT FK_message_messageCategory FOREIGN KEY (messageCategoryID) REFERENCES tbl_messageCategory(messageCategoryID)
)
go

--PRODUCT
CREATE TABLE tbl_product
(
productID bigint not null primary key identity(1,1),
name nvarchar(150),
price float,
priceSale float,
dayStartSale Date,
dayEndSale Date,
image varchar(MAX),
isActive bit,
isFeatured bit,
createdDate Date,
modifiedDate Date,
quantity int,
metaTitle nvarchar(500),
description nvarchar(MAX),
metaKeyword nvarchar(250),
countView int,
categoryID bigint not null

CONSTRAINT FK_product_category FOREIGN KEY (categoryID) REFERENCES tbl_category(categoryID)
)
go

--CUSTOMER
CREATE TABLE tbl_customer
(
 customerID bigint not null primary key identity(1,1),
 firstName nvarchar(100),
 lastName nvarchar(150),
 birthDay Date,
 gender bit,
 phoneNumber varchar(20),
 address nvarchar(250),
 email varchar(100),
 password varchar(700),
 isActive bit,
 isDelete bit,
 createdDate Date,
 ModifiedDate Date,
 tokenID varchar(500),
 roleID bigint not null

 CONSTRAINT FK_customer_role FOREIGN KEY (roleID) REFERENCES tbl_roles(roleID)
)
go

--ORDER
CREATE TABLE tbl_order
(
orderID bigint not null primary key identity(1,1),
userName nvarchar(250),
dateOfStart Date,
dateOffinish Date,
status bit,
shippingTypeID bigint not null,
customerID bigint not null,
promotionPrice float,
paymentTypeID bigint not null

CONSTRAINT FK_order_shippingType FOREIGN KEY (shippingTypeID) REFERENCES tbl_shippingType(shippingTypeID),
CONSTRAINT FK_order_customer FOREIGN KEY (customerID) REFERENCES tbl_customer(customerID),
CONSTRAINT FK_order_paymentType FOREIGN KEY (paymentTypeID) REFERENCES tbl_paymentType(paymentTypeID)
)
go

--DISTRICT
CREATE TABLE tbl_district
(
 districtID bigint not null primary key identity(1,1),
 name nvarchar(250),
 code varchar(150),
 isActive bit,
 isDelete bit,
 cityID bigint not null

CONSTRAINT FK_district_city FOREIGN KEY (cityID) REFERENCES tbl_city(cityID),
)
go

--ORDER DETAIL
CREATE TABLE tbl_orderDetail
(
orderDetailID bigint not null primary key identity(1,1),
productID bigint not null,
messageID bigint not null,
districtID bigint not null,
orderID bigint not null,
unitPrice float,
address nvarchar(500),
note nvarchar(500),
phone varchar(50),
quantity int


CONSTRAINT FK_orderDetail_product FOREIGN KEY (productID) REFERENCES tbl_product(productID),
CONSTRAINT FK_orderDetail_message FOREIGN KEY (messageID) REFERENCES tbl_message(messageID),
CONSTRAINT FK_orderDetail_district FOREIGN KEY (districtID) REFERENCES tbl_district(districtID),
CONSTRAINT FK_orderDetail_order FOREIGN KEY (orderID) REFERENCES tbl_order(orderID),
)
go
--IMAGE LIST
CREATE TABLE tbl_imageList
(
imageListID bigint not null primary key identity(1,1),
name varchar(700),
extendsion varchar(50),
productID bigint not null


CONSTRAINT FK_imageList_product FOREIGN KEY (productID) REFERENCES tbl_product(productID)
)
go

--PROCEDURE FIND CATEGORY BY NAME
CREATE PROCEDURE GetCategoryByName
@_name nvarchar(max)
AS
BEGIN
  SELECT * FROM tbl_category WHERE name = @_name
END
go