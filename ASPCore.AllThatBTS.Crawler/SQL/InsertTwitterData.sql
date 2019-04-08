﻿INSERT INTO TB_TWITTER_DATA
(
	ID
	,TWITTER_ID
	,ACCOUNT_NAME
	,TWEET_TEXT
	,HASHTAGS
	,RETWEET_CNT
	,URL
	,DELETED_YN
	,DELETED_DT
	,CREATE_DT
	,UPDATE_DT
) 
VALUES (
	(SELECT UPPER(LEFT(UUID(), 8)))
	,@TWITTER_ID
	,@ACCOUNT_NAME
	,@TWEET_TEXT
	,@HASHTAGS
	,@RETWEET_CNT
	,@URL
	,'N'
	,NULL
	,(SELECT NOW())
	,NULL
)