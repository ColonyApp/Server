DROP DATABASE [ColonyDB]
GO
CREATE DATABASE [ColonyDB]
GO
ALTER DATABASE [ColonyDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled')) 
begin
EXEC [ColonyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ColonyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ColonyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ColonyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ColonyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ColonyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ColonyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ColonyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ColonyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ColonyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ColonyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ColonyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ColonyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ColonyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ColonyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ColonyDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ColonyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ColonyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ColonyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ColonyDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [ColonyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ColonyDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ColonyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ColonyDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ColonyDB] SET  MULTI_USER 
GO
ALTER DATABASE [ColonyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ColonyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ColonyDB] SET QUERY_STORE = OFF
GO
USE [ColonyDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE [ColonyDB] SET  READ_WRITE 
GO
