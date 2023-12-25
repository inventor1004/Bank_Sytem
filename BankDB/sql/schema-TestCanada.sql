DROP DATABASE IF EXISTS TestCanada;
CREATE DATABASE TestCanada;
Use TestCanada;

DROP TABLE IF EXISTS `Province`;
CREATE TABLE `Province` (
	Province_Id VARCHAR(3) PRIMARY KEY,
    Province_Name VARCHAR(25) UNIQUE NOT NULL,
    Capital_City VARCHAR(25) UNIQUE NOT NULL
);

INSERT INTO `Province` (Province_Id, Province_Name, Capital_City) 
VALUES ('AB', 'Alberta', 'Edmonton'),
	   ('BC', 'British Columbia', 'Victoria'),
       ('MB', 'Manitoba', 'Winnipeg'),
       ('NB', 'New Brunswick', 'Fredericton'),
       ('NL', 'Newfoundland and Labrador', 'St. John''s'),
       ('NS', 'Nova Scotia', 'Halifax'),
       ('ON', 'Ontario', 'Toronto'),
       ('PEI', 'Prince Edward Island', 'Charlottetown'),
       ('QC', 'Quebec', 'Quebec City'),
       ('SK', 'Saskatchewan', 'Regina'),
       ('YT', 'Yukon', 'Whitehorse'),
       ('NU', 'Nunavut', 'Iqaluit'),
       ('NT', 'Northwest Territories', 'Yellowknife');