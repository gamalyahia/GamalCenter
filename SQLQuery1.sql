use GamalCenter;

drop table if exists Courses;
drop table if exists student;
drop table if exists logindet;


create table Courses 
(
Courses_ID int primary key identity(1,1) not null , 
Courses_name varchar(100),
Courses_price int,
);

create table student
(
Student_ID int primary key identity(1,1) not null , 
Student_name varchar(100) unique,
Student_adress varchar(100),
Student_age int,
Student_phone varchar(100)unique,
Courses_ID int foreign key (Courses_ID) REFERENCES Courses(Courses_ID),
);

create table logindet
(
ID int primary key identity(1,1) not null , 
username varchar(100) unique not null , 
Passwordx varchar(100) not null, 
)