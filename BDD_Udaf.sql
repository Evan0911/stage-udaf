-- --------------------------------------------------------
-- Hôte :                        localhost
-- Version du serveur:           5.7.24 - MySQL Community Server (GPL)
-- SE du serveur:                Win64
-- HeidiSQL Version:             10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Listage de la structure de la base pour udaf
CREATE DATABASE IF NOT EXISTS `udaf` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `udaf`;

-- Listage de la structure de la table udaf. article
CREATE TABLE IF NOT EXISTS `article` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `auteur` varchar(50) NOT NULL,
  `date_creation` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `titre` varchar(100) NOT NULL DEFAULT '',
  `contenu` varchar(5000) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Listage des données de la table udaf.article : ~3 rows (environ)
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` (`id`, `auteur`, `date_creation`, `titre`, `contenu`) VALUES
	(1, 'Gérard Menvussa', '2021-01-26 08:37:02', 'Test article', 'Ceci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'articleCeci est un test d\'article'),
	(2, 'Gérard Menvussa', '2021-02-04 10:32:23', '04/02, deuxième test', 'Test n°2');
/*!40000 ALTER TABLE `article` ENABLE KEYS */;

-- Listage de la structure de la table udaf. metier
CREATE TABLE IF NOT EXISTS `metier` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `libelle` char(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Listage des données de la table udaf.metier : ~2 rows (environ)
/*!40000 ALTER TABLE `metier` DISABLE KEYS */;
INSERT INTO `metier` (`id`, `libelle`) VALUES
	(1, 'general'),
	(2, 'comptable'),
	(3, 'vendeur');
/*!40000 ALTER TABLE `metier` ENABLE KEYS */;

-- Listage de la structure de la table udaf. raccourci
CREATE TABLE IF NOT EXISTS `raccourci` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` char(50) NOT NULL DEFAULT '',
  `chemin` varchar(500) NOT NULL DEFAULT '',
  `image` varchar(500) NOT NULL DEFAULT '',
  `idMetier` int(11) NOT NULL DEFAULT '0',
  `colonne` int(11) NOT NULL DEFAULT '0',
  `ligne` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;

-- Listage des données de la table udaf.raccourci : ~7 rows (environ)
/*!40000 ALTER TABLE `raccourci` DISABLE KEYS */;
INSERT INTO `raccourci` (`id`, `nom`, `chemin`, `image`, `idMetier`, `colonne`, `ligne`) VALUES
	(7, 'Google Chrome', 'C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe', 'C:\\Users\\evan\\Documents\\Images\\chromelogo.png', 1, 1, 2),
	(8, 'Discord', 'C:\\Users\\evan\\AppData\\Local\\Discord\\Update.exe', '', 2, 3, 3),
	(10, 'OBS', 'C:\\Program Files (x86)\\OBS\\OBS.exe', '', 2, 1, 2),
	(11, 'Steam', 'D:\\Nouveau dossier (2)\\Program Files (x86)\\Steam\\Steam.exe', '', 3, 5, 5),
	(12, 'Spotify', 'C:\\Users\\evan\\AppData\\Roaming\\Spotify\\Spotify.exe', '', 3, 1, 2),
	(13, 'ProjetUDAF.sln', 'D:\\bureau\\Stage UDAF\\ProjetUDAF\\ProjetUDAF.sln', '', 1, 1, 5);
/*!40000 ALTER TABLE `raccourci` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
