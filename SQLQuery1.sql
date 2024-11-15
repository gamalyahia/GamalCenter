use GamalCenter;

drop table if exists Courses;
drop table if exists student;
drop table if exists logindet;


create table Courses
(
Courses_ID int primary key identity(1,1) not null , 
Courses_Grade varchar(100) unique,
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
Courses_name int foreign key (Courses_name) REFERENCES Courses(Courses_ID),
Courses_Grade int foreign key (Courses_Grade) REFERENCES Courses(Courses_ID),
);

create table logindet
(
ID int primary key identity(1,1) not null , 
username varchar(100) unique not null , 
Passwordx varchar(100) not null, 
)

insert into Courses(Courses_name , Courses_price , Courses_Grade)values
('Arabic' , 1200 , 'Grade 5')


select * from Courses;
select * from logindet;
select * from student;