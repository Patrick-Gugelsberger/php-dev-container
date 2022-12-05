CREATE SCHEMA `whiffersnifferdb` ;

CREATE TABLE `whiffersnifferdb`.`SensorConfigurations` (
  `Id` INT NOT NULL DEFAULT 1,
  `ReadInterval` INT NULL,
  `SyncInterval` INT NULL,
  `WarningValue` INT NULL,
  `CriticalValue` INT NULL,
  `Brightness` INT NULL,
  `DiodMode` INT NULL);

  CREATE TABLE `whiffersnifferdb`.`SensorReadings` (
  `Id` CHAR(36) BINARY NOT NULL,
  `Name` VARCHAR(45) NULL,
  `Time` DATETIME NULL,
  `Co2Value` DOUBLE NULL,
  `TemperatureValue` DOUBLE NULL,
  `HumidityValue` DOUBLE NULL,
  PRIMARY KEY (`Id`));