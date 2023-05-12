CREATE DATABASE localCourseDb;
USE localCourseDb;

CREATE TABLE Courses (
    CourseID int IDENTITY(1,1),
    Title NVARCHAR(255),
	StartDate DATE,
	EndDate DATE,
	Description NVARCHAR(255),
	TheoryHours INT,
	PracticeHours INT,
	Approved INT,
	PRIMARY KEY (CourseID)
);

INSERT INTO Courses (Title, StartDate, EndDate, Description, TheoryHours, PracticeHours, Approved)
VALUES ('First course', GETDATE(), GETDATE(), 'Instructor', 3, 4, 0);
INSERT INTO Courses (Title, StartDate, EndDate, Description, TheoryHours, PracticeHours, Approved)
VALUES ('Second course', GETDATE(), GETDATE(), 'Instructor', 3, 4, 0);
INSERT INTO Courses (Title, StartDate, EndDate, Description, TheoryHours, PracticeHours, Approved)
VALUES ('Third course', GETDATE(), GETDATE(), 'Instructor', 3, 4, 0);

CREATE TABLE PrerequisiteCourses (
	ID int IDENTITY(1,1),
	CourseID INT,
	PrerequisiteID INT,
	PRIMARY KEY (ID)
);

INSERT INTO PrerequisiteCourses (CourseID, PrerequisiteID)
VALUES (2, 3);

UPDATE Courses
SET PrerequisiteCourses = 2
WHERE CourseID = 4;

ALTER TABLE PrerequisiteCourses
ADD CONSTRAINT FK_Course
FOREIGN KEY (CourseID) REFERENCES Courses(CourseID);

ALTER TABLE PrerequisiteCourses
ADD CONSTRAINT FK_CoursesRequired
FOREIGN KEY (PrerequisiteID) REFERENCES Courses(CourseID);

--Droping tables
drop table Courses;
drop table PrerequisiteCourses;

--Droping Prerequisite FK
ALTER TABLE PrerequisiteCourses
DROP CONSTRAINT FK_Course;
ALTER TABLE PrerequisiteCourses
DROP CONSTRAINT FK_CoursesRequired;

--Droping FKs 
ALTER TABLE SkillsTrainedByCourse
DROP CONSTRAINT FK_CourseTraining;
ALTER TABLE SkillsTrainedByCourse
DROP CONSTRAINT FK_SkillTrainedByCourse;

CREATE TABLE Users (
    ID INT IDENTITY(1, 1),
    Username NVARCHAR(100),
    Password varchar(255),
	Email varchar(30),
	Role varchar(10),
	PRIMARY KEY (ID)
);

CREATE TABLE Skills (
    SkillID INT IDENTITY(1, 1),
    SkillName NVARCHAR(100),
	PRIMARY KEY (SkillID)
);

CREATE TABLE SkillsTrainedByCourse (
	CourseId INT,
    SkillID INT,
    Level NVARCHAR(100),
	PRIMARY KEY (CourseId, SkillID)
);

INSERT INTO SkillsTrainedByCourse (CourseId, SkillID, Level)
VALUES (1, 1, 'Junior developer');
--FK of SkillsTrainedByCourse
ALTER TABLE SkillsTrainedByCourse
ADD CONSTRAINT FK_CourseTraining
FOREIGN KEY (CourseId) REFERENCES Courses(CourseId);
ALTER TABLE SkillsTrainedByCourse
ADD CONSTRAINT FK_SkillTrainedByCourse
FOREIGN KEY (SkillID) REFERENCES Skills(SkillID);

--

INSERT INTO Skills (SkillName)
VALUES ('Java Programming');

INSERT INTO Users (Username, Password, Email, Role)
VALUES ('Test2', 'test', 'test@gmail.com', 'Instructor');

