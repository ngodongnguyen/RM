create database RM;
go
use RM
go
create table users(
userID int primary key identity,
username nvarchar(100),
upass nvarchar(100),
uName nvarchar(100),
uphone nvarchar(100)
)
create table tables(
tid int primary key identity,
tname  nvarchar (15)
)

create table staff (
staffID int primary key identity,
sName nvarchar (50),
sPhone nvarchar (50),
sRole nvarchar (50)
)
create table category(
catID int primary key identity,
catName nvarchar(100)
)
create table products(
pID int primary key identity,
pName nvarchar(50),
pPrice float,
categoryID int,
pImage image,
foreign key (categoryID) references category(catID) -- Khóa ngo?i tr? d?n b?ng category

)
create table tblMain
(
MainID int primary key identity,
aDate datetime,
tTime nvarchar(15),
TableName nvarchar(10),
WaiterName nvarchar(15),
status nvarchar(15),
orderType nvarchar(15),
total float,
received float,
change float,
aTime nvarchar(100),
driverID int,
CustName nvarchar(100),
CustPhone nvarchar(100)
)


create table tblDetails
(
DetailID int primary key identity,
MainID int,
proID int,
qty int,
price float,
amount float,
foreign key (MainID) references tblMain(MainID) -- Khóa ngo?i tr? d?n b?ng tblMain
)
create table Promotion(
promotion_id int primary key identity,
promotion_name nvarchar(100),
discount_value int,
status nvarchar(100)
)
-- Thêm 5 ngu?i dùng vào b?ng users
INSERT INTO users (username, upass, uName, uphone) VALUES
('user1', 'pass1', 'Nguyen Van A', '0901234567'),
('user2', 'pass2', 'Tran Thi B', '0909876543'),
('user3', 'pass3', 'Le Hoang C', '0911223344'),
('user4', 'pass4', 'Pham Thu D', '0988776655'),
('user5', 'pass5', 'Vu Duc E', '0933445566'),
('admin','123','Ngo Dong Nguyen','0383377696');
-- Thêm 10 bàn vào b?ng tables
INSERT INTO tables (tname) VALUES
('Table 1'), ('Table 2'), ('Table 3'), ('Table 4'), ('Table 5'),
('Table 6'), ('Table 7'), ('Table 8'), ('Table 9'), ('Table 10');
-- Thêm 30 nhân viên vào b?ng staff (6 cho m?i vai trò)
INSERT INTO staff (sName, sPhone, sRole) VALUES
('Cashier1', '0901112222', 'Cashier'),
('Cashier2', '0903334444', 'Cashier'),
('Cashier3', '0905556666', 'Cashier'),
('Cashier4', '0907778888', 'Cashier'),
('Cashier5', '0909990000', 'Cashier'),
('Cashier6', '0912223333', 'Cashier'),
('Driver1', '0914445555', 'Driver'),
('Driver2', '0916667777', 'Driver'),
('Driver3', '0918889999', 'Driver'),
('Driver4', '0921112222', 'Driver'),
('Driver5', '0923334444', 'Driver'),
('Driver6', '0925556666', 'Driver'),
('Waiter1', '0927778888', 'Waiter'),
('Waiter2', '0929990000', 'Waiter'),
('Waiter3', '0932223333', 'Waiter'),
('Waiter4', '0934445555', 'Waiter'),
('Waiter5', '0936667777', 'Waiter'),
('Waiter6', '0938889999', 'Waiter'),
('Manager1', '0941112222', 'Manager'),
('Manager2', '0943334444', 'Manager'),
('Manager3', '0945556666', 'Manager'),
('Manager4', '0947778888', 'Manager'),
('Manager5', '0949990000', 'Manager'),
('Manager6', '0952223333', 'Manager'),
('Cleaning1', '0954445555', 'Cleaning'),
('Cleaning2', '0956667777', 'Cleaning'),
('Cleaning3', '0958889999', 'Cleaning'),
('Cleaning4', '0961112222', 'Cleaning'),
('Cleaning5', '0963334444', 'Cleaning'),
('Cleaning6', '0965556666', 'Cleaning');
-- Thêm 5 danh m?c vào b?ng category
INSERT INTO category (catName) VALUES
('Appetizers'),
('Main course'),
('Dessert'),
('Drink'),
('Special Dish');
-- Thêm 10 chuong trình khuy?n mãi vào b?ng Promotion
select * from products
INSERT INTO Promotion (promotion_name, discount_value, status) VALUES
('20k discount for member customers', 20000, 'Active'),
('Weekend offer: 1 free drink', 30000, 'Active'),
('Free shipping for orders over 300k', 35000, 'Active'), -- Coi nhu gi?m 100% phí giao hàng
('Special discount for new items', 50000, 'Active'),
('Accumulate points to redeem gifts: 10 points get 50k discount', 50000, 'expired'), -- Gi?m giá c? d?nh khi d? di?m
('Customer Birthday', 700000, 'Active');
select * from category
SELECT    c.catName,p.pName,    SUM(od.amount) AS TotalRevenue 
FROM     tblDetails od JOIN   Products p ON od.proID = p.pID 
JOIN   tblMain t ON od.MainID = t.MainID 
join category c on c.catID=p.categoryID
WHERE    t.status='Paid' and od.MainID!=0
GROUP BY    p.pID, p.pName, p.pPrice,c.catName
select * from tblMain
INSERT INTO tblMain (aDate, tTime, TableName, WaiterName, status, orderType, total, received, change, aTime, driverID, CustName, CustPhone)
VALUES
('2025/07/15', NULL, 'Table 7', 'Waiter3', 'Unpaid', 'Din In', 345000, 0, 0, '01:15 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/28', NULL, 'Table 2', 'Waiter1', 'Paid', 'Take Away', 8790000, 9000000, 210000, '08:52 PM', 0, 'Le Hoang C', '0911223344'),
('2025/11/09', NULL, 'Table 5', 'Waiter5', 'Complete', 'Delivery', 1230000, 1230000, 0, '06:30 PM', 3, 'Pham Ngoc D', '0977889900'),
('2025/04/21', NULL, 'Table 10', 'Waiter2', 'Cancelled', 'Din In', 567000, 0, 0, '11:01 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/09/07', NULL, 'Table 3', 'Waiter4', 'Paid', 'Take Away', 2100000, 2500000, 400000, '03:48 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/03/18', NULL, 'Table 8', 'Waiter1', 'Unpaid', 'Delivery', 987000, 0, 0, '09:25 PM', 6, 'Tran Thi B', '0987654321'),
('2025/12/25', NULL, 'Table 1', 'Waiter3', 'Complete', 'Din In', 456000, 456000, 0, '07:05 PM', 0, 'Le Hoang C', '0911223344'),
('2025/06/12', NULL, 'Table 6', 'Waiter5', 'Cancelled', 'Take Away', 1500000, 0, 0, '02:12 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/01/29', NULL, 'Table 4', 'Waiter2', 'Paid', 'Delivery', 678000, 700000, 22000, '10:39 AM', 1, 'Hoang Minh E', '0333444555'),
('2025/10/10', NULL, 'Table 9', 'Waiter4', 'Unpaid', 'Din In', 1122000, 0, 0, '04:56 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/08/16', NULL, 'Table 2', 'Waiter1', 'Complete', 'Take Away', 789000, 789000, 0, '12:08 PM', 0, 'Tran Thi B', '0987654321'),
('2025/05/26', NULL, 'Table 7', 'Waiter3', 'Cancelled', 'Delivery', 1800000, 0, 0, '07:44 PM', 5, 'Le Hoang C', '0911223344'),
('2025/11/13', NULL, 'Table 5', 'Waiter5', 'Paid', 'Din In', 321000, 350000, 29000, '03:21 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/03/30', NULL, 'Table 10', 'Waiter2', 'Unpaid', 'Take Away', 901000, 0, 0, '11:58 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/07/08', NULL, 'Table 3', 'Waiter4', 'Complete', 'Delivery', 1345000, 1345000, 0, '05:13 PM', 2, 'Nguyen Van A', '0901234567'),
('2025/01/19', NULL, 'Table 8', 'Waiter1', 'Cancelled', 'Din In', 654000, 0, 0, '09:02 PM', 0, 'Tran Thi B', '0987654321'),
('2025/10/27', NULL, 'Table 1', 'Waiter3', 'Paid', 'Take Away', 2211000, 2250000, 39000, '06:47 PM', 0, 'Le Hoang C', '0911223344'),
('2025/05/14', NULL, 'Table 6', 'Waiter5', 'Unpaid', 'Delivery', 1678000, 0, 0, '01:35 PM', 7, 'Pham Ngoc D', '0977889900'),
('2025/08/31', NULL, 'Table 4', 'Waiter2', 'Complete', 'Din In', 432000, 432000, 0, '10:16 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/02/11', NULL, 'Table 9', 'Waiter4', 'Cancelled', 'Take Away', 1012000, 0, 0, '04:33 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/09/17', NULL, 'Table 2', 'Waiter1', 'Paid', 'Delivery', 890000, 950000, 60000, '12:50 PM', 4, 'Tran Thi B', '0987654321'),
('2025/04/24', NULL, 'Table 7', 'Waiter3', 'Unpaid', 'Din In', 398000, 0, 0, '08:29 PM', 0, 'Le Hoang C', '0911223344'),
('2025/12/15', NULL, 'Table 5', 'Waiter5', 'Complete', 'Take Away', 1111000, 1111000, 0, '06:07 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/06/22', NULL, 'Table 10', 'Waiter2', 'Cancelled', 'Delivery', 543000, 0, 0, '10:44 AM', 6, 'Hoang Minh E', '0333444555'),
('2025/03/09', NULL, 'Table 3', 'Waiter4', 'Paid', 'Din In', 2345000, 2400000, 55000, '03:05 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/07/20', NULL, 'Table 8', 'Waiter1', 'Unpaid', 'Take Away', 912000, 0, 0, '09:48 PM', 0, 'Tran Thi B', '0987654321'),
('2025/01/28', NULL, 'Table 1', 'Waiter3', 'Complete', 'Delivery', 1456000, 1456000, 0, '07:22 PM', 1, 'Le Hoang C', '0911223344'),
('2025/10/16', NULL, 'Table 6', 'Waiter5', 'Cancelled', 'Din In', 1789000, 0, 0, '02:59 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/05/07', NULL, 'Table 4', 'Waiter2', 'Paid', 'Take Away', 765000, 800000, 35000, '10:03 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/08/12', NULL, 'Table 9', 'Waiter4', 'Unpaid', 'Delivery', 1234000, 0, 0, '04:10 PM', 3, 'Nguyen Van A', '0901234567'),
('2025/02/18', NULL, 'Table 2', 'Waiter1', 'Complete', 'Din In', 876000, 876000, 0, '12:25 PM', 0, 'Tran Thi B', '0987654321'),
('2025/11/25', NULL, 'Table 7', 'Waiter3', 'Cancelled', 'Take Away', 1900000, 0, 0, '07:57 PM', 0, 'Le Hoang C', '0911223344'),
('2025/04/13', NULL, 'Table 5', 'Waiter5', 'Paid', 'Delivery', 432000, 450000, 18000, '03:38 PM', 5, 'Pham Ngoc D', '0977889900'),
('2025/09/30', NULL, 'Table 10', 'Waiter2', 'Unpaid', 'Din In', 1098000, 0, 0, '11:35 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/03/08', NULL, 'Table 3', 'Waiter4', 'Complete', 'Take Away', 1567000, 1567000, 0, '05:30 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/07/19', NULL, 'Table 8', 'Waiter1', 'Cancelled', 'Delivery', 789000, 0, 0, '09:15 PM', 7, 'Tran Thi B', '0987654321'),
('2025/01/27', NULL, 'Table 1', 'Waiter3', 'Paid', 'Din In', 2123000, 2150000, 27000, '06:30 PM', 0, 'Le Hoang C', '0911223344'),
('2025/10/14', NULL, 'Table 6', 'Waiter5', 'Unpaid', 'Take Away', 1890000, 0, 0, '01:52 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/05/31', NULL, 'Table 4', 'Waiter2', 'Complete', 'Delivery', 543000, 543000, 0, '10:33 AM', 2, 'Hoang Minh E', '0333444555'),
('2025/08/11', NULL, 'Table 9', 'Waiter4', 'Cancelled', 'Din In', 1100000, 0, 0, '04:47 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/02/17', NULL, 'Table 2', 'Waiter1', 'Paid', 'Take Away', 901000, 1000000, 99000, '12:33 PM', 0, 'Tran Thi B', '0987654321'),
('2025/11/24', NULL, 'Table 7', 'Waiter3', 'Unpaid', 'Delivery', 456000, 0, 0, '08:12 PM', 4, 'Le Hoang C', '0911223344'),
('2025/04/15', NULL, 'Table 5', 'Waiter5', 'Complete', 'Din In', 1222000, 1222000, 0, '05:50 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/09/22', NULL, 'Table 10', 'Waiter2', 'Cancelled', 'Take Away', 654000, 0, 0, '11:01 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/03/09', NULL, 'Table 3', 'Waiter4', 'Paid', 'Delivery', 2456000, 2500000, 44000, '03:18 PM', 6, 'Nguyen Van A', '0901234567'),
('2025/07/20', NULL, 'Table 8', 'Waiter1', 'Unpaid', 'Din In', 1023000, 0, 0, '09:31 PM', 0, 'Tran Thi B', '0987654321'),
('2025/01/28', NULL, 'Table 1', 'Waiter3', 'Complete', 'Take Away', 1567000, 1567000, 0, '07:35 PM', 0, 'Le Hoang C', '0911223344'),
('2025/10/16', NULL, 'Table 6', 'Waiter5', 'Cancelled', 'Delivery', 1901000, 0, 0, '02:42 PM', 1, 'Pham Ngoc D', '0977889900'),
('2025/05/07', NULL, 'Table 4', 'Waiter2', 'Paid', 'Din In', 876000, 900000, 24000, '10:16 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/08/12', NULL, 'Table 9', 'Waiter4', 'Unpaid', 'Take Away', 1345000, 0, 0, '04:23 PM', 0, 'Nguyen Van A', '0901234567')
INSERT INTO tblMain (aDate, tTime, TableName, WaiterName, status, orderType, total, received, change, aTime, driverID, CustName, CustPhone)
VALUES
('2025/06/05', NULL, 'Table 3', 'Waiter2', 'Complete', 'Take Away', 1543000, 1543000, 0, '07:37 PM', 0, 'Le Hoang C', '0911223344'),
('2025/12/01', NULL, 'Table 8', 'Waiter5', 'Complete', 'Din In', 890000, 890000, 0, '03:00 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/01/19', NULL, 'Table 1', 'Waiter1', 'Complete', 'Delivery', 1789000, 1789000, 0, '10:24 AM', 4, 'Hoang Minh E', '0333444555'),
('2025/09/07', NULL, 'Table 6', 'Waiter4', 'Complete', 'Take Away', 1123000, 1123000, 0, '04:47 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/04/24', NULL, 'Table 9', 'Waiter3', 'Complete', 'Din In', 1567000, 1567000, 0, '12:29 PM', 0, 'Tran Thi B', '0987654321'),
('2025/07/12', NULL, 'Table 2', 'Waiter2', 'Complete', 'Delivery', 1012000, 1012000, 0, '07:15 PM', 1, 'Le Hoang C', '0911223344'),
('2025/12/03', NULL, 'Table 5', 'Waiter5', 'Complete', 'Take Away', 2012000, 2012000, 0, '03:42 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/03/27', NULL, 'Table 10', 'Waiter1', 'Complete', 'Din In', 543000, 543000, 0, '12:08 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/08/15', NULL, 'Table 4', 'Waiter3', 'Complete', 'Delivery', 1432000, 1432000, 0, '08:31 PM', 6, 'Nguyen Van A', '0901234567'),
('2025/05/04', NULL, 'Table 7', 'Waiter2', 'Complete', 'Take Away', 876000, 876000, 0, '01:54 PM', 0, 'Tran Thi B', '0987654321'),
('2025/10/22', NULL, 'Table 3', 'Waiter5', 'Complete', 'Din In', 1333000, 1333000, 0, '06:17 PM', 0, 'Le Hoang C', '0911223344'),
('2025/02/10', NULL, 'Table 8', 'Waiter1', 'Complete', 'Delivery', 1901000, 1901000, 0, '09:40 AM', 3, 'Pham Ngoc D', '0977889900'),
('2025/11/28', NULL, 'Table 1', 'Waiter4', 'Complete', 'Take Away', 654000, 654000, 0, '04:03 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/04/16', NULL, 'Table 6', 'Waiter3', 'Complete', 'Din In', 1234000, 1234000, 0, '11:26 AM', 0, 'Nguyen Van A', '0901234567'),
('2025/07/05', NULL, 'Table 9', 'Waiter2', 'Complete', 'Delivery', 1012000, 1012000, 0, '07:49 PM', 7, 'Tran Thi B', '0987654321'),
('2025/12/31', NULL, 'Table 2', 'Waiter5', 'Complete', 'Take Away', 1345000, 1345000, 0, '03:00 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/28', NULL, 'Table 5', 'Waiter1', 'Complete', 'Din In', 654000, 654000, 0, '11:09 AM', 0, 'Pham Ngoc D', '0977889900'),
('2025/08/16', NULL, 'Table 10', 'Waiter4', 'Complete', 'Delivery', 1567000, 1567000, 0, '08:44 PM', 2, 'Hoang Minh E', '0333444555'),
('2025/05/05', NULL, 'Table 4', 'Waiter3', 'Complete', 'Take Away', 876000, 876000, 0, '02:07 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/23', NULL, 'Table 7', 'Waiter2', 'Complete', 'Din In', 1345000, 1345000, 0, '06:47 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/11', NULL, 'Table 3', 'Waiter5', 'Complete', 'Delivery', 1100000, 1100000, 0, '09:53 AM', 5, 'Le Hoang C', '0911223344'),
('2025/11/29', NULL, 'Table 8', 'Waiter1', 'Complete', 'Take Away', 1567000, 1567000, 0, '03:36 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/17', NULL, 'Table 1', 'Waiter4', 'Complete', 'Din In', 765000, 765000, 0, '11:22 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/09/30', NULL, 'Table 6', 'Waiter3', 'Complete', 'Delivery', 1211000, 1211000, 0, '09:03 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/19', NULL, 'Table 9', 'Waiter2', 'Complete', 'Take Away', 901000, 901000, 0, '02:20 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/10', NULL, 'Table 2', 'Waiter5', 'Complete', 'Din In', 1456000, 1456000, 0, '06:58 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/29', NULL, 'Table 5', 'Waiter1', 'Complete', 'Delivery', 765000, 765000, 0, '11:22 AM', 2, 'Pham Ngoc D', '0977889900'),
('2025/08/17', NULL, 'Table 10', 'Waiter4', 'Complete', 'Take Away', 1678000, 1678000, 0, '08:57 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/05/06', NULL, 'Table 4', 'Waiter3', 'Complete', 'Din In', 987000, 987000, 0, '02:19 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/24', NULL, 'Table 7', 'Waiter2', 'Complete', 'Delivery', 1433000, 1433000, 0, '07:00 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/12', NULL, 'Table 3', 'Waiter5', 'Complete', 'Take Away', 1212000, 1212000, 0, '10:06 AM', 5, 'Le Hoang C', '0911223344'),
('2025/12/01', NULL, 'Table 8', 'Waiter1', 'Complete', 'Din In', 1678000, 1678000, 0, '03:49 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/18', NULL, 'Table 1', 'Waiter4', 'Complete', 'Delivery', 876000, 876000, 0, '11:35 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/10/01', NULL, 'Table 6', 'Waiter3', 'Complete', 'Take Away', 1323000, 1323000, 0, '09:16 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/20', NULL, 'Table 9', 'Waiter2', 'Complete', 'Din In', 1012000, 1012000, 0, '02:33 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/11', NULL, 'Table 2', 'Waiter5', 'Complete', 'Delivery', 1567000, 1567000, 0, '07:11 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/30', NULL, 'Table 5', 'Waiter1', 'Complete', 'Take Away', 765000, 765000, 0, '11:35 AM', 2, 'Pham Ngoc D', '0977889900'),
('2025/08/18', NULL, 'Table 10', 'Waiter4', 'Complete', 'Din In', 1789000, 1789000, 0, '09:10 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/05/07', NULL, 'Table 4', 'Waiter3', 'Complete', 'Delivery', 1098000, 1098000, 0, '02:32 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/25', NULL, 'Table 7', 'Waiter2', 'Complete', 'Take Away', 1543000, 1543000, 0, '07:13 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/13', NULL, 'Table 3', 'Waiter5', 'Complete', 'Din In', 1323000, 1323000, 0, '10:19 AM', 5, 'Le Hoang C', '0911223344'),
('2025/12/02', NULL, 'Table 8', 'Waiter1', 'Complete', 'Delivery', 1789000, 1789000, 0, '04:02 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/19', NULL, 'Table 1', 'Waiter4', 'Complete', 'Take Away', 987000, 987000, 0, '11:48 AM', 0, 'Hoang Minh E', '0333444555'),
('2025/10/02', NULL, 'Table 6', 'Waiter3', 'Complete', 'Din In', 1456000, 1456000, 0, '09:29 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/21', NULL, 'Table 9', 'Waiter2', 'Complete', 'Delivery', 1122000, 1122000, 0, '02:46 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/12', NULL, 'Table 2', 'Waiter5', 'Complete', 'Take Away', 1678000, 1678000, 0, '07:24 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/31', NULL, 'Table 5', 'Waiter1', 'Complete', 'Din In', 876000, 876000, 0, '11:48 AM', 2, 'Pham Ngoc D', '0977889900'),
('2025/08/19', NULL, 'Table 10', 'Waiter4', 'Complete', 'Delivery', 1901000, 1901000, 0, '09:23 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/05/08', NULL, 'Table 4', 'Waiter3', 'Complete', 'Take Away', 1211000, 1211000, 0, '02:45 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/26', NULL, 'Table 7', 'Waiter2', 'Complete', 'Din In', 1654000, 1654000, 0, '07:26 PM', 0, 'Tran Thi B', '0987654321')
INSERT INTO tblMain (aDate, tTime, TableName, WaiterName, status, orderType, total, received, change, aTime, driverID, CustName, CustPhone)
VALUES
('2025/06/06', NULL, 'Table 3', 'Waiter2', 'Paid', 'Take Away', 1654000, 1700000, 46000, '07:50 PM', 0, 'Le Hoang C', '0911223344'),
('2025/12/02', NULL, 'Table 8', 'Waiter5', 'Paid', 'Din In', 987000, 1000000, 13000, '03:13 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/01/20', NULL, 'Table 1', 'Waiter1', 'Paid', 'Delivery', 1890000, 1900000, 10000, '10:37 AM', 4, 'Hoang Minh E', '0333444555'),
('2025/09/08', NULL, 'Table 6', 'Waiter4', 'Paid', 'Take Away', 1234000, 1250000, 16000, '05:00 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/04/25', NULL, 'Table 9', 'Waiter3', 'Paid', 'Din In', 1678000, 1700000, 22000, '12:42 PM', 0, 'Tran Thi B', '0987654321'),
('2025/07/13', NULL, 'Table 2', 'Waiter2', 'Paid', 'Delivery', 1123000, 1150000, 27000, '07:28 PM', 1, 'Le Hoang C', '0911223344'),
('2025/12/04', NULL, 'Table 5', 'Waiter5', 'Paid', 'Take Away', 2123000, 2150000, 27000, '03:55 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/03/29', NULL, 'Table 10', 'Waiter1', 'Paid', 'Din In', 654000, 700000, 46000, '12:21 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/08/16', NULL, 'Table 4', 'Waiter3', 'Paid', 'Delivery', 1543000, 1600000, 57000, '08:44 PM', 6, 'Nguyen Van A', '0901234567'),
('2025/05/05', NULL, 'Table 7', 'Waiter2', 'Paid', 'Take Away', 987000, 1000000, 13000, '02:07 PM', 0, 'Tran Thi B', '0987654321'),
('2025/10/23', NULL, 'Table 3', 'Waiter5', 'Paid', 'Din In', 1456000, 1500000, 44000, '07:00 PM', 0, 'Le Hoang C', '0911223344'),
('2025/02/12', NULL, 'Table 8', 'Waiter1', 'Paid', 'Delivery', 2012000, 2050000, 38000, '09:53 AM', 3, 'Pham Ngoc D', '0977889900'),
('2025/11/30', NULL, 'Table 1', 'Waiter4', 'Paid', 'Take Away', 765000, 800000, 35000, '04:16 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/04/18', NULL, 'Table 6', 'Waiter3', 'Paid', 'Din In', 1345000, 1400000, 55000, '11:39 AM', 0, 'Nguyen Van A', '0901234567'),
('2025/07/07', NULL, 'Table 9', 'Waiter2', 'Paid', 'Delivery', 1122000, 1150000, 28000, '08:02 PM', 7, 'Tran Thi B', '0987654321'),
('2025/12/05', NULL, 'Table 2', 'Waiter5', 'Paid', 'Take Away', 1456000, 1500000, 44000, '03:13 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/30', NULL, 'Table 5', 'Waiter1', 'Paid', 'Din In', 765000, 800000, 35000, '11:48 AM', 0, 'Pham Ngoc D', '0977889900'),
('2025/08/17', NULL, 'Table 10', 'Waiter4', 'Paid', 'Delivery', 1901000, 1950000, 49000, '09:00 PM', 2, 'Hoang Minh E', '0333444555'),
('2025/05/06', NULL, 'Table 4', 'Waiter3', 'Paid', 'Take Away', 1098000, 1100000, 2000, '02:32 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/24', NULL, 'Table 7', 'Waiter2', 'Paid', 'Din In', 1543000, 1600000, 57000, '07:13 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/13', NULL, 'Table 3', 'Waiter5', 'Paid', 'Delivery', 1323000, 1350000, 27000, '10:06 AM', 5, 'Le Hoang C', '0911223344'),
('2025/12/03', NULL, 'Table 8', 'Waiter1', 'Paid', 'Take Away', 1789000, 1800000, 11000, '04:15 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/19', NULL, 'Table 1', 'Waiter4', 'Paid', 'Din In', 987000, 1000000, 13000, '12:01 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/10/02', NULL, 'Table 6', 'Waiter3', 'Paid', 'Delivery', 1456000, 1500000, 44000, '09:16 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/21', NULL, 'Table 9', 'Waiter2', 'Paid', 'Take Away', 1234000, 1250000, 16000, '02:46 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/12', NULL, 'Table 2', 'Waiter5', 'Paid', 'Din In', 1678000, 1700000, 22000, '07:24 PM', 0, 'Le Hoang C', '0911223344'),
('2025/03/31', NULL, 'Table 5', 'Waiter1', 'Paid', 'Delivery', 876000, 900000, 24000, '11:48 AM', 2, 'Pham Ngoc D', '0977889900'),
('2025/08/18', NULL, 'Table 10', 'Waiter4', 'Paid', 'Take Away', 2012000, 2050000, 38000, '09:23 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/05/07', NULL, 'Table 4', 'Waiter3', 'Paid', 'Din In', 1211000, 1250000, 39000, '02:45 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/25', NULL, 'Table 7', 'Waiter2', 'Paid', 'Delivery', 1654000, 1700000, 46000, '07:26 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/14', NULL, 'Table 3', 'Waiter5', 'Paid', 'Take Away', 1432000, 1450000, 18000, '10:19 AM', 5, 'Le Hoang C', '0911223344'),
('2025/12/04', NULL, 'Table 8', 'Waiter1', 'Paid', 'Din In', 1901000, 1900000, -1000, '04:28 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/20', NULL, 'Table 1', 'Waiter4', 'Paid', 'Delivery', 1098000, 1100000, 2000, '12:14 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/10/03', NULL, 'Table 6', 'Waiter3', 'Paid', 'Take Away', 1567000, 1600000, 33000, '09:42 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/22', NULL, 'Table 9', 'Waiter2', 'Paid', 'Din In', 1345000, 1350000, 5000, '03:00 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/13', NULL, 'Table 2', 'Waiter5', 'Paid', 'Delivery', 1789000, 1800000, 11000, '07:37 PM', 0, 'Le Hoang C', '0911223344'),
('2025/04/01', NULL, 'Table 5', 'Waiter1', 'Paid', 'Take Away', 987000, 1000000, 13000, '12:01 PM', 2, 'Pham Ngoc D', '0977889900'),
('2025/08/19', NULL, 'Table 10', 'Waiter4', 'Paid', 'Din In', 2123000, 2150000, 27000, '09:36 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/05/08', NULL, 'Table 4', 'Waiter3', 'Paid', 'Delivery', 1322000, 1350000, 28000, '03:00 PM', 0, 'Nguyen Van A', '0901234567'),
('2025/10/26', NULL, 'Table 7', 'Waiter2', 'Paid', 'Take Away', 1765000, 1800000, 35000, '07:39 PM', 0, 'Tran Thi B', '0987654321'),
('2025/02/15', NULL, 'Table 3', 'Waiter5', 'Paid', 'Din In', 1543000, 1600000, 57000, '10:32 AM', 5, 'Le Hoang C', '0911223344'),
('2025/12/05', NULL, 'Table 8', 'Waiter1', 'Paid', 'Delivery', 2012000, 2000000, -12000, '04:41 PM', 0, 'Pham Ngoc D', '0977889900'),
('2025/04/21', NULL, 'Table 1', 'Waiter4', 'Paid', 'Take Away', 1211000, 1250000, 39000, '12:27 PM', 0, 'Hoang Minh E', '0333444555'),
('2025/10/04', NULL, 'Table 6', 'Waiter3', 'Paid', 'Din In', 1678000, 1700000, 22000, '09:55 PM', 7, 'Nguyen Van A', '0901234567'),
('2025/07/23', NULL, 'Table 9', 'Waiter2', 'Paid', 'Delivery', 1456000, 1500000, 44000, '03:13 PM', 0, 'Tran Thi B', '0987654321'),
('2025/12/14', NULL, 'Table 2', 'Waiter5', 'Paid', 'Take Away', 1890000, 1900000, 10000, '07:50 PM', 0, 'Le Hoang C', '0911223344'),
('2025/04/02', NULL, 'Table 5', 'Waiter1', 'Paid', 'Din In', 1098000, 1100000, 2000, '12:14 PM', 2, 'Pham Ngoc D', '0977889900')
SELECT     Month(aDate),    COUNT(MainID) AS OrderCount FROM     tblMain WHERE     status = 'Paid' GROUP BY     Month(aDate)
 SELECT "products"."pName", "products"."pPrice", "products"."pImage"
 FROM   "RM"."dbo"."products" "products"
 select * from tblMain
select * from  tblMain m inner join tblDetails d on m.MainID = d.MainID inner join products p on p.pID = d.proID inner join category c on c.catID =p.categoryID where m.aDate between '25/08/15' and '25/12/05'

select* from tblMain
