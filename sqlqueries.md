```SQL
CREATE TABLE vehicle
(
    id integer NOT NULL Primary Key,
    vehiclename character varying(50),
    vehiclename character varying(14)   
);

CREATE TABLE containers
(
    id integer NOT NULL Primary Key,
    containername character varying(50),
    latitude double PRECISION,
    longitude double PRECISION,
    vehicleid integer  REFERENCES vehicle (id)
);

 INSERT INTO vehicle
  VALUES (1,'mercedes', '42a45'),
        (2,'volvo','51b65'),
        (3,'bmc','44s54'),
        (4,'ford','47hy45');

INSERT INTO containers
  VALUES (1,'abc', 12.3,3.4,1),
  (2,'bcd', 8.4,5.7,1),
  (3,'cde', 9.5,9.3,1),
  (4,'def', 6.4,2.4,1),
  (5,'feg', 8.2,9.4,1),
  (6,'egh', 9.4,6.8,1),
  (7,'ghi', 3.4,7.9,1),
  (8,'hij', 2.8,3.8,1);

  INSERT INTO containers
  VALUES (9,'ijk', 54.3,4.8,2),
  (10,'jkl', 4.9,7.9,2),
  (11,'klm', 3.8,1.9,2),
  (12,'lmn', 9.2,8.4,2),
  (13,'mno', 3.4,3.4,2),
  (14,'nop', 12.8,89.8,2),
  (15,'opr', 9.2,25.9,2),
  (16,'prs', 14.8,1.8,2);

  INSERT INTO containers
  VALUES (17,'rst', 18.3,2.4,3),
  (18,'stu', 3.2,7.7,3),
  (19,'tuv', 1.2,8.4,3),
  (20,'uvy', 25.5,9.4,3),
  (21,'vyz', 1.9,2.4,3),
  (22,'yza', 8.5,6.8,3),
  (23,'zab', 3.9,8.9,3),
  (24,'abc', 12.8,3.5,3);

 INSERT INTO containers
  VALUES (25,'bcd', 58.3,4.4,4),
  (26,'cde', 62.5,5.5,4),
  (27,'def', 58.5,9.6,4),
  (28,'efg', 6.7,8.4,4),
  (29,'fgh', 3.7,69.4,4),
  (30,'ghi,', 1.9,23.8,4),
  (31,'hij', 1.89,12.9,4),
  (32,'ijk', 24.4,8.8,4);
```
