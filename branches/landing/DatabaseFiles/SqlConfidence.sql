/****** Object:  View [dbo].[vwExercisesQuestionsUsers]    Script Date: 24/02/2014 20:45:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP view [dbo].[vwExercisesQuestionsUsers]
GO
CREATE view [dbo].[vwExercisesQuestionsUsers]
as

select 
exq.*,
vUsers.us_id,
exqa.exqa_created_date 'AnsweredDate',
cast(case isnull(exqa.us_id, -1) when -1 then 0 else 1 end as bit) 'Answered'
from exercises_questions exq
join (
select * from users 
union
select -1, 'Not ', 'Logged In', 'xxx@sqlconfidence.com', 'xxxxxx', getdate(), 'system', null, null, null, null, null, null, null
 ) vUsers on 1=1
left join exercises_questions_answered exqa on exq.exq_id = exqa.exq_id and vUsers.us_id = exqa.us_id
where exq.exq_deleted = 0
