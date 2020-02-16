-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.5.0-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              10.2.0.5683
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Дамп структуры базы данных cartridges
CREATE DATABASE IF NOT EXISTS `cartridges` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cartridges`;

-- Дамп структуры для таблица cartridges.cartridges
CREATE TABLE IF NOT EXISTS `cartridges` (
  `Order` int(11) DEFAULT NULL,
  `Seal` int(11) DEFAULT NULL,
  `Model` varchar(254) DEFAULT NULL,
  `Organization` varchar(254) DEFAULT NULL,
  `Info` varchar(254) DEFAULT NULL,
  `Data` varchar(254) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL,
  `LSC` varchar(254) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица cartridges.cartridgesmodels
CREATE TABLE IF NOT EXISTS `cartridgesmodels` (
  `Model` varchar(50) DEFAULT NULL,
  `Vendor` varchar(50) DEFAULT NULL,
  `Toner` varchar(50) DEFAULT NULL,
  `ImageUrl` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица cartridges.organization
CREATE TABLE IF NOT EXISTS `organization` (
  `Organization` varchar(254) DEFAULT NULL,
  `Phone` varchar(254) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица cartridges.refill
CREATE TABLE IF NOT EXISTS `refill` (
  `Order` int(11) NOT NULL AUTO_INCREMENT,
  `Seal` int(11) DEFAULT NULL,
  `Accepted` varchar(254) DEFAULT NULL,
  `Filled` varchar(254) DEFAULT NULL,
  `Replaced` varchar(254) DEFAULT NULL,
  `Note` varchar(254) DEFAULT NULL,
  PRIMARY KEY (`Order`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
