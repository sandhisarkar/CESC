-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.50


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema cesc_db
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ cesc_db;
USE cesc_db;

--
-- Table structure for table `cesc_db`.`ac_resource`
--

DROP TABLE IF EXISTS `ac_resource`;
CREATE TABLE `ac_resource` (
  `resource_id` varchar(50) NOT NULL DEFAULT '',
  `resource_des` varchar(200) DEFAULT NULL,
  `SrlNo` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`resource_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`ac_resource`
--

/*!40000 ALTER TABLE `ac_resource` DISABLE KEYS */;
INSERT INTO `ac_resource` (`resource_id`,`resource_des`,`SrlNo`) VALUES 
 ('aboutToolStripMenuItem','About',23),
 ('batchSummeryToolStripMenuItem','Batch Summary',21),
 ('batchToolStripMenuItem','New Batch Create',3),
 ('boxSummaryToolStripMenuItem','UAT Report',20),
 ('changePasswordToolStripMenuItem','Change Password',17),
 ('configurationToolStripMenuItem','Configuration',16),
 ('expertQualityControlCentreToolStripMenuItem','Expert QC',11),
 ('exportToolStripMenuItem','Export',12),
 ('imageQCToolStripMenuItem','Image QC',9),
 ('indexingToolStripMenuItem','Indexing',10),
 ('itemsToolStripMenuItem','New Item Create',1),
 ('lICToolStripMenuItem','LIC Audit',14),
 ('mnuJobCreation','Job Creation',7),
 ('newUserToolStripMenuItem','Create New User',18),
 ('projectToolStripMenuItem','New Project Create',2),
 ('projectToolStripMenuItem1','Inventory In',6),
 ('reexportToolStripMenuItem','Re Export',22),
 ('reportsToolStripMenuItem','Reports',19),
 ('toolsToolStripMenuItem','Tools',15),
 ('toolStripMenuItem1','Batch Scanning',8),
 ('toolStripMenuItem2','LIC',13);
INSERT INTO `ac_resource` (`resource_id`,`resource_des`,`SrlNo`) VALUES 
 ('transactinToolStripMenuItem','Transaction',4),
 ('uploadCSVToolStripMenuItem','Upload CSV',5),
 ('userManualToolStripMenuItem','User Manual',24);
/*!40000 ALTER TABLE `ac_resource` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`ac_role`
--

DROP TABLE IF EXISTS `ac_role`;
CREATE TABLE `ac_role` (
  `role_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`ac_role`
--

/*!40000 ALTER TABLE `ac_role` DISABLE KEYS */;
INSERT INTO `ac_role` (`role_id`,`role_description`) VALUES 
 (1,'Scan'),
 (2,'Admin'),
 (3,'QC'),
 (4,'Supervisor'),
 (5,'Indexing'),
 (6,'InventoryIn'),
 (7,'DP'),
 (8,'IGR Audit');
/*!40000 ALTER TABLE `ac_role` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`ac_role_resource_map`
--

DROP TABLE IF EXISTS `ac_role_resource_map`;
CREATE TABLE `ac_role_resource_map` (
  `sr_no` int(11) NOT NULL AUTO_INCREMENT,
  `role_id` int(11) DEFAULT NULL,
  `resource_id` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`sr_no`),
  KEY `ac_role_resource_map_ibfk_2` (`resource_id`),
  KEY `ac_role_resource_map_ibfk_1` (`role_id`),
  CONSTRAINT `ac_role_resource_map_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `ac_role` (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`ac_role_resource_map`
--

/*!40000 ALTER TABLE `ac_role_resource_map` DISABLE KEYS */;
INSERT INTO `ac_role_resource_map` (`sr_no`,`role_id`,`resource_id`) VALUES 
 (12,1,'transactinToolStripMenuItem'),
 (13,1,'toolStripMenuItem1'),
 (14,1,'toolStripButton1'),
 (15,3,'transactinToolStripMenuItem'),
 (16,3,'imageQCToolStripMenuItem'),
 (17,3,'toolStripButton3'),
 (21,5,'transactinToolStripMenuItem'),
 (22,5,'indexingToolStripMenuItem'),
 (23,5,'toolStripButton2'),
 (24,4,'transactinToolStripMenuItem'),
 (25,4,'projectToolStripMenuItem1'),
 (26,4,'toolStripMenuItem1'),
 (27,4,'mnuJobCreation'),
 (28,4,'imageQCToolStripMenuItem'),
 (29,4,'indexingToolStripMenuItem'),
 (30,4,'expertQualityControlCentreToolStripMenuItem'),
 (31,4,'exportToolStripMenuItem'),
 (32,4,'lICToolStripMenuItem'),
 (33,4,'toolStripMenuItem2'),
 (34,4,'reportsToolStripMenuItem'),
 (35,4,'reexportToolStripMenuItem'),
 (36,4,'batchSummeryToolStripMenuItem'),
 (37,6,'transactinToolStripMenuItem'),
 (38,6,'projectToolStripMenuItem1'),
 (39,7,'transactinToolStripMenuItem'),
 (40,7,'mnuJobCreation'),
 (41,4,'toolStripButton1'),
 (42,4,'toolStripButton2');
INSERT INTO `ac_role_resource_map` (`sr_no`,`role_id`,`resource_id`) VALUES 
 (43,4,'toolStripButton3'),
 (44,4,'boxSummaryToolStripMenuItem'),
 (45,4,'aboutToolStripMenuItem'),
 (46,4,'userManualToolStripMenuItem'),
 (47,1,'toolsToolStripMenuItem'),
 (48,1,'aboutToolStripMenuItem'),
 (49,1,'userManualToolStripMenuItem'),
 (50,3,'changePasswordToolStripMenuItem'),
 (51,3,'toolsToolStripMenuItem'),
 (52,3,'aboutToolStripMenuItem'),
 (53,3,'userManualToolStripMenuItem'),
 (54,5,'toolsToolStripMenuItem'),
 (55,5,'changePasswordToolStripMenuItem'),
 (56,5,'helpToolStripMenuItem'),
 (57,5,'aboutToolStripMenuItem'),
 (58,5,'userManualToolStripMenuItem'),
 (59,4,'helpToolStripMenuItem'),
 (60,1,'helpToolStripMenuItem'),
 (61,1,'changePasswordToolStripMenuItem'),
 (62,3,'helpToolStripMenuItem'),
 (63,8,'toolStripMenuItem2'),
 (64,8,'lICToolStripMenuItem'),
 (65,8,'helpToolStripMenuItem'),
 (66,8,'aboutToolStripMenuItem'),
 (67,8,'userManualToolStripMenuItem');
/*!40000 ALTER TABLE `ac_role_resource_map` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`ac_user`
--

DROP TABLE IF EXISTS `ac_user`;
CREATE TABLE `ac_user` (
  `user_id` varchar(30) NOT NULL DEFAULT '',
  `user_name` varchar(100) DEFAULT NULL,
  `user_pwd` varchar(50) NOT NULL DEFAULT '',
  `logged` int(1) NOT NULL DEFAULT '0',
  `logged_dttm` datetime DEFAULT NULL,
  `last_activity` varchar(40) DEFAULT NULL,
  `current_activity` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`ac_user`
--

/*!40000 ALTER TABLE `ac_user` DISABLE KEYS */;
INSERT INTO `ac_user` (`user_id`,`user_name`,`user_pwd`,`logged`,`logged_dttm`,`last_activity`,`current_activity`) VALUES 
 ('amit','AMIT GHOSH','amit123',1,'2020-03-03 16:55:29','',''),
 ('amit1','AMIT GHOSH','amit123',0,NULL,'',''),
 ('anita','anita sur','ani123',0,NULL,'',''),
 ('bikram','bikram chatterjee','bik123',0,NULL,'',''),
 ('ibrahim','ibrahim sk','ibr123',0,NULL,'',''),
 ('joyasri','joyasri chaiyal','joy123',0,NULL,'',''),
 ('madhumitabiswas','Madhumita Biswas','Madhumita@1',0,NULL,'',''),
 ('moumita1','moumita1','mou123',0,NULL,'',''),
 ('prem','prem','pre123',0,NULL,'',''),
 ('priya','priya golder','pri123',0,NULL,'',''),
 ('rumachandra','Ruma Chandra','Kolkata?37',0,NULL,'',''),
 ('sharbari','sharbari','saha123',0,NULL,'',''),
 ('souvik','souvikdas','sou123',0,NULL,'',''),
 ('u1','u1','111111',0,NULL,NULL,NULL),
 ('u6','tanmoy','zzzzzz',0,NULL,NULL,NULL);
/*!40000 ALTER TABLE `ac_user` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`ac_user_role_map`
--

DROP TABLE IF EXISTS `ac_user_role_map`;
CREATE TABLE `ac_user_role_map` (
  `sr_no` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` varchar(30) DEFAULT NULL,
  `role_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`sr_no`),
  KEY `FK_ac_user_role_map` (`user_id`),
  KEY `ac_user_role_map_ibfk_2` (`role_id`),
  CONSTRAINT `ac_user_role_map_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `ac_role` (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=158 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`ac_user_role_map`
--

/*!40000 ALTER TABLE `ac_user_role_map` DISABLE KEYS */;
INSERT INTO `ac_user_role_map` (`sr_no`,`user_id`,`role_id`) VALUES 
 (35,'u1',2),
 (36,'u2',1),
 (37,'u3',3),
 (38,'u4',4),
 (39,'u5',5),
 (40,'u6',8),
 (41,'sdas',2),
 (42,'dass',4),
 (43,'usc',1),
 (44,'uinv',6),
 (45,'udp',1),
 (46,'uqc',3),
 (47,'uidx',5),
 (48,'ulic',8),
 (49,'ldas',1),
 (50,'pnath',3),
 (51,'rbiswas',5),
 (52,'msinha',3),
 (53,'udeb',5),
 (54,'niladri',3),
 (55,'sandipk',3),
 (56,'malakarm',1),
 (57,'ulodh',3),
 (58,'subrata',4),
 (59,'jdas',5),
 (60,'pratima',4),
 (61,'schakraborty',8),
 (62,'rbiswasqc',3),
 (63,'jdasqc',3),
 (64,'udebqc',3),
 (65,'rbqc',3),
 (66,'malakar',3),
 (67,'hirokhkd',2),
 (68,'bdebroy',8),
 (69,'gdas',8),
 (70,'manmohan',8),
 (71,'sujit',3),
 (72,'dasj',4),
 (73,'mmalakar',3),
 (74,'piya',5),
 (75,'prakash',3),
 (76,'udedfqc',4),
 (77,'rbiswasfqc',4),
 (78,'piyafqc',4),
 (79,'tp',2),
 (80,'kshamke',8),
 (81,'sujit1',1),
 (82,'mdc',3),
 (83,'jsb',3),
 (84,'mm',3),
 (85,'ad',3),
 (86,'kc',3),
 (87,'bs',3),
 (88,'kn',3),
 (89,'kclicin',5),
 (90,'ns',3);
INSERT INTO `ac_user_role_map` (`sr_no`,`user_id`,`role_id`) VALUES 
 (91,'knath',3),
 (92,'sizu',2),
 (93,'amit',5),
 (94,'gayatri',3),
 (95,'biswami',3),
 (96,'shelly',3),
 (97,'monirul',3),
 (98,'ujjwal',5),
 (99,'uttam',3),
 (100,'rahul',3),
 (101,'sandip',3),
 (102,'kurmi',5),
 (103,'abhishek',3),
 (104,'pritam',3),
 (105,'mdcindex',5),
 (106,'nil',1),
 (107,'jddas',4),
 (108,'lic',8),
 (109,'sanjoy',3),
 (110,'ranjan',3),
 (111,'vikki',3),
 (112,'test',3),
 (113,'paltu',1),
 (114,'sanjoyb',8),
 (115,'paltuqc',3),
 (116,'skbose',8),
 (117,'vikkis',4),
 (118,'mousumid',3),
 (119,'paltuf',4),
 (120,'amitavad',8),
 (121,'shazia',2),
 (122,'nepal',1),
 (123,'scan',1),
 (124,'shazia1',2),
 (125,'balaram',1),
 (126,'anup',1),
 (127,'sazia',4),
 (128,'surojit',4),
 (129,'raju',1),
 (130,'mintu',1),
 (131,'singh',1),
 (132,'kumar',1),
 (133,'chandan',1),
 (134,'rajan',4),
 (135,'dipak',1),
 (136,'jugal',1),
 (137,'rajusardar',1),
 (138,'ajoy',4),
 (139,'ajoy1',2),
 (140,'dipanwita',4),
 (141,'raju1',4),
 (142,'rajkumar',3);
INSERT INTO `ac_user_role_map` (`sr_no`,`user_id`,`role_id`) VALUES 
 (143,'moujit1',4),
 (144,'sukla',4),
 (145,'prem',4),
 (146,'amit',4),
 (147,'amit1',4),
 (148,'sharbari',4),
 (149,'anita',4),
 (150,'priya',4),
 (151,'moumita1',4),
 (152,'souvik',4),
 (153,'rumachandra',8),
 (154,'madhumitabiswas',8),
 (155,'bikram',4),
 (156,'ibrahim',4),
 (157,'joyasri',4);
/*!40000 ALTER TABLE `ac_user_role_map` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`batch_master`
--

DROP TABLE IF EXISTS `batch_master`;
CREATE TABLE `batch_master` (
  `batch_key` int(11) NOT NULL AUTO_INCREMENT,
  `proj_code` int(11) NOT NULL DEFAULT '0',
  `Batch_code` varchar(50) NOT NULL,
  `batch_name` varchar(50) DEFAULT NULL,
  `Created_By` varchar(100) NOT NULL DEFAULT '',
  `Created_DTTM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `Modified_by` varchar(100) DEFAULT NULL,
  `Modified_DTTM` datetime DEFAULT NULL,
  `Batch_path` varchar(100) NOT NULL DEFAULT '',
  `status` int(2) NOT NULL DEFAULT '0',
  `Active` int(11) DEFAULT NULL,
  `run_no` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`batch_key`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`batch_master`
--

/*!40000 ALTER TABLE `batch_master` DISABLE KEYS */;
/*!40000 ALTER TABLE `batch_master` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`document_master`
--

DROP TABLE IF EXISTS `document_master`;
CREATE TABLE `document_master` (
  `doc_id` int(50) NOT NULL AUTO_INCREMENT,
  `doc_type` varchar(100) NOT NULL,
  PRIMARY KEY (`doc_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`document_master`
--

/*!40000 ALTER TABLE `document_master` DISABLE KEYS */;
INSERT INTO `document_master` (`doc_id`,`doc_type`) VALUES 
 (1,'File Note'),
 (2,'Correspondance/Letter'),
 (3,'Notice (Newspaper)'),
 (4,'Order'),
 (5,'Petition'),
 (6,'Brochure'),
 (7,'Regulation'),
 (8,'Receipt'),
 (9,'Order');
/*!40000 ALTER TABLE `document_master` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`image_import`
--

DROP TABLE IF EXISTS `image_import`;
CREATE TABLE `image_import` (
  `proj_key` int(11) NOT NULL DEFAULT '0',
  `batch_key` int(11) NOT NULL DEFAULT '0',
  `filename` varchar(500) NOT NULL DEFAULT '0',
  `serial_no` varchar(50) NOT NULL DEFAULT '0',
  `page_index_name` varchar(100) DEFAULT NULL,
  `created_by` varchar(100) NOT NULL DEFAULT '',
  `created_dttm` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_dttm` datetime DEFAULT '0000-00-00 00:00:00',
  `Page_name` varchar(100) NOT NULL DEFAULT '',
  `status` int(2) NOT NULL DEFAULT '0',
  `Doc_Type` varchar(30) DEFAULT NULL,
  `SCanned_size` double NOT NULL DEFAULT '0',
  `QC_size` double NOT NULL DEFAULT '0',
  `fqc_size` double NOT NULL DEFAULT '0',
  `index_size` double NOT NULL DEFAULT '0',
  `Photo` int(1) NOT NULL DEFAULT '0',
  `Image_seq` int(4) DEFAULT NULL,
  PRIMARY KEY (`proj_key`,`batch_key`,`filename`,`serial_no`,`Page_name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`image_import`
--

/*!40000 ALTER TABLE `image_import` DISABLE KEYS */;
/*!40000 ALTER TABLE `image_import` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`metadata_entry`
--

DROP TABLE IF EXISTS `metadata_entry`;
CREATE TABLE `metadata_entry` (
  `proj_key` varchar(50) NOT NULL,
  `batch_key` varchar(50) NOT NULL,
  `sl_no` varchar(100) DEFAULT NULL,
  `item_no` varchar(100) DEFAULT NULL,
  `reference_no` varchar(500) DEFAULT NULL,
  `issue_from` varchar(100) DEFAULT NULL,
  `issue_to` varchar(100) DEFAULT NULL,
  `sub_cat` varchar(100) DEFAULT NULL,
  `sub_name` varchar(1000) DEFAULT NULL,
  `doc_type` varchar(100) DEFAULT NULL,
  `issue_date` varchar(100) DEFAULT NULL,
  `filename` varchar(500) DEFAULT NULL,
  `created_by` varchar(100) DEFAULT NULL,
  `created_dttm` varchar(100) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`metadata_entry`
--

/*!40000 ALTER TABLE `metadata_entry` DISABLE KEYS */;
INSERT INTO `metadata_entry` (`proj_key`,`batch_key`,`sl_no`,`item_no`,`reference_no`,`issue_from`,`issue_to`,`sub_cat`,`sub_name`,`doc_type`,`issue_date`,`filename`,`created_by`,`created_dttm`,`status`) VALUES 
 ('1','1','1','1','AVC1','G1','KJ','APR','hjvb','File Note','2000-01-11','AVC1','u1','2022-07-16','Imported'),
 ('1','1','0','2','HF','HMGF','NV','APR','khk','File Note','2000-01-11','HF','u1','2022-07-16','Imported'),
 ('1','1','0','3','AVC2','DKJ','SDK','APR','kj','File Note','1990-01-23','AVC2','u1','2022-07-16','Imported');
/*!40000 ALTER TABLE `metadata_entry` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`project_master`
--

DROP TABLE IF EXISTS `project_master`;
CREATE TABLE `project_master` (
  `proj_key` int(11) NOT NULL AUTO_INCREMENT,
  `Proj_Code` varchar(100) NOT NULL DEFAULT '',
  `Project_Path` varchar(200) NOT NULL DEFAULT '',
  `Created_By` varchar(100) NOT NULL DEFAULT '',
  `Created_DTTM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `Modified_By` varchar(100) DEFAULT NULL,
  `Modified_DTTM` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `status` int(2) DEFAULT NULL,
  PRIMARY KEY (`proj_key`,`Proj_Code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`project_master`
--

/*!40000 ALTER TABLE `project_master` DISABLE KEYS */;
/*!40000 ALTER TABLE `project_master` ENABLE KEYS */;


--
-- Table structure for table `cesc_db`.`sub_cat_master`
--

DROP TABLE IF EXISTS `sub_cat_master`;
CREATE TABLE `sub_cat_master` (
  `sub_cat_id` int(50) NOT NULL AUTO_INCREMENT,
  `sub_cat_name` varchar(100) NOT NULL,
  PRIMARY KEY (`sub_cat_id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cesc_db`.`sub_cat_master`
--

/*!40000 ALTER TABLE `sub_cat_master` DISABLE KEYS */;
INSERT INTO `sub_cat_master` (`sub_cat_id`,`sub_cat_name`) VALUES 
 (1,'Tariff related Petition/order'),
 (2,'Other Petition/Order'),
 (3,'Fuel Documents'),
 (4,'Agreement-power purchase'),
 (5,'Agreement-other'),
 (6,'PMS'),
 (7,'Other HR'),
 (8,'Reporting'),
 (9,'Act/Policy/Regulations'),
 (10,'Customer Tariff Grievance'),
 (11,'Regulatory /Legal Proceedings'),
 (12,'Other');
/*!40000 ALTER TABLE `sub_cat_master` ENABLE KEYS */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
