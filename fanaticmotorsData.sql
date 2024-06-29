-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 29-06-2024 a las 13:47:25
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `fanaticmotors`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `awards`
--

CREATE TABLE `awards` (
  `Award_Id` int(11) NOT NULL,
  `Award_Pilot` int(11) NOT NULL,
  `Award_Team` int(11) NOT NULL,
  `Award_Type` int(11) NOT NULL,
  `Award_Season` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `award_types`
--

CREATE TABLE `award_types` (
  `Award_Type_Id` int(11) NOT NULL,
  `Award_Type_Name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pilots`
--

CREATE TABLE `pilots` (
  `Pilot_Id` int(11) NOT NULL,
  `Pilot_Team` int(11) NOT NULL,
  `Pilot_Name` varchar(100) NOT NULL,
  `Pilot_Surname` varchar(200) NOT NULL,
  `Pilot_Status` enum('competing','retired') NOT NULL,
  `Pilot_Number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `races`
--

CREATE TABLE `races` (
  `Race_Id` int(11) NOT NULL,
  `Race_Circuit` int(11) NOT NULL,
  `Race_Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `results`
--

CREATE TABLE `results` (
  `Result_Id` int(11) NOT NULL,
  `Result_Race` int(11) NOT NULL,
  `Result_Pilot` int(11) NOT NULL,
  `Result_Team` int(11) NOT NULL,
  `Result_Position` int(11) NOT NULL,
  `Result_Points` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `seasons`
--

CREATE TABLE `seasons` (
  `Season_Id` int(11) NOT NULL,
  `Season_Name` varchar(50) NOT NULL,
  `Season_Start` date NOT NULL,
  `Season_End` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `teams`
--

CREATE TABLE `teams` (
  `Team_Id` int(11) NOT NULL,
  `Team_Name` varchar(100) NOT NULL,
  `Team_Status` enum('competing','retired') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `user_Id` varchar(100) NOT NULL,
  `user_name` varchar(100) NOT NULL,
  `user_surnames` varchar(100) NOT NULL,
  `user_date` date NOT NULL,
  `user_password` varchar(100) NOT NULL,
  `user_kind` enum('user','admin') DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`user_Id`, `user_name`, `user_surnames`, `user_date`, `user_password`, `user_kind`) VALUES
('ibai.a', 'ibai', 'Alvarez', '2024-05-08', 'zqPvHFHh7Be1ZiuARLLBGo3HLkknuUs9r20AxUOZJrs=', 'admin');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `awards`
--
ALTER TABLE `awards`
  ADD PRIMARY KEY (`Award_Id`);

--
-- Indices de la tabla `award_types`
--
ALTER TABLE `award_types`
  ADD PRIMARY KEY (`Award_Type_Id`);

--
-- Indices de la tabla `pilots`
--
ALTER TABLE `pilots`
  ADD PRIMARY KEY (`Pilot_Id`);

--
-- Indices de la tabla `races`
--
ALTER TABLE `races`
  ADD PRIMARY KEY (`Race_Id`);

--
-- Indices de la tabla `results`
--
ALTER TABLE `results`
  ADD PRIMARY KEY (`Result_Id`);

--
-- Indices de la tabla `seasons`
--
ALTER TABLE `seasons`
  ADD PRIMARY KEY (`Season_Id`);

--
-- Indices de la tabla `teams`
--
ALTER TABLE `teams`
  ADD PRIMARY KEY (`Team_Id`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `awards`
--
ALTER TABLE `awards`
  MODIFY `Award_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `award_types`
--
ALTER TABLE `award_types`
  MODIFY `Award_Type_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `pilots`
--
ALTER TABLE `pilots`
  MODIFY `Pilot_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `races`
--
ALTER TABLE `races`
  MODIFY `Race_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `results`
--
ALTER TABLE `results`
  MODIFY `Result_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `seasons`
--
ALTER TABLE `seasons`
  MODIFY `Season_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `teams`
--
ALTER TABLE `teams`
  MODIFY `Team_Id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
