USE [E_Vision_Task]
GO
/****** Object:  StoredProcedure [dbo].[SP_DiconnectedVehicles]    Script Date: 12/26/2020 5:45:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Ahmed Elhaggar>
-- Create date: <2020-04-19>
-- Description:	<Get Dicconected Vehicle>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DiconnectedVehicles] @TotalSeccond INT ,@Res NVARCHAR(MAX) OUTPUT

AS
	Begin  
	  
		SELECT   Distinct Vehicle.Id INTO #Result
		FROM Vehicle
		INNER JOIN VehicleTracking On Vehicle.Id=VehicleTracking.VehicleId

		WHERE DATEDIFF(SECOND,GetUtcDate(),(Select top 1 RequestTime  from VehicleTracking order by RequestTime desc)) < @TotalSeccond
	 select @Res= stuff((
        select ','+convert(varchar(10),Id)
        from #Result
        for xml path (''), type).value('.','nvarchar(max)')
      ,1,1,'')

	End



