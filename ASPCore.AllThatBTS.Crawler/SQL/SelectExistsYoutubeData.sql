﻿SELECT CASE WHEN EXISTS(
			SELECT YOUTUBE_ID 
			  FROM TB_YOUTUBE_DATA
			 WHERE YOUTUBE_ID = @YOUTUBE_ID
			) THEN 'true'
	   ELSE 'false' END AS result;