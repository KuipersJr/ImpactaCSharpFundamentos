﻿ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [ImpactaCSharp1_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.LDF', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

