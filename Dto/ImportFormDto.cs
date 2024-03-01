using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConvert.Dto
{
    public class ImportFormDto
    {
        public string FormIdentifier { get; set; }

        public string VesselCode { get; set; }

        public string ImoNumber { get; set; }

        public string VesselName { get; set; }

        public string ReportDateTime { get; set; }

        public string VoyageNo { get; set; }

        public string Port { get; set; }

        public string Slip { get; set; }

        public string ChartererCleaningKitOnboard { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ETD { get; set; }

        public string Remarks { get; set; }

        public string CPSpeed { get; set; }

        public string ObsSpeed { get; set; }

        public string ObservedDistance { get; set; }

        public string EngineDistance { get; set; }

        public string MECounter { get; set; }

        public string MEOutputPct { get; set; }

        public string IncineratorHrs { get; set; }

        public string GenFWHrs { get; set; }

        public string Gen1Hrs { get; set; }

        public string Gen2Hrs { get; set; }

        public string Gen3Hrs { get; set; }

        public string Gen4Hrs { get; set; }

        public string MEKWhrs { get; set; }

        public string Gen1KWhrs { get; set; }

        public string Gen2KWhrs { get; set; }

        public string Gen3KWhrs { get; set; }

        public string Gen4KWhrs { get; set; }

        public string MainEngineHrs { get; set; }

        public string RPM { get; set; }

        public string FWDDraft { get; set; }

        public string MIDDraft { get; set; }

        public string AFTDraft { get; set; }

        public string Heading { get; set; }

        public string SteamingHrs { get; set; }

        public string DWT { get; set; }

        public string Displacement { get; set; }

        public string VesselCondition { get; set; }

        public decimal? Constant { get; set; }

        public string AirTemp { get; set; }

        public string BaroPressure { get; set; }

        public string SeaState { get; set; }

        public string Swell { get; set; }

        public string WindForce { get; set; }

        public string SeaTemp { get; set; }

        public string SeaDir { get; set; }

        public string SwellDir { get; set; }

        public string WindDirection { get; set; }

        public string SeaHeight { get; set; }

        public string SwellHeight { get; set; }

        public string FreshWaterCon { get; set; }

        public string FreshWaterROB { get; set; }

        public string SlopsROB { get; set; }

        public string ChartererCleaningChemicalsROB { get; set; }

        public string Air_cooler_tempin_degC { get; set; }

        public string Air_cooler_tempout_degC { get; set; }

        public string Average_Speed { get; set; }

        public string Berthing_Prospect { get; set; }

        public string DailyFWProduction { get; set; }

        public string Dist_by_Speed_Log_nm { get; set; }

        public string DistanceToGO { get; set; }

        public string DropAfterAirCooler { get; set; }

        public string Exh_gas_tempin_degC { get; set; }

        public string Exh_gas_tempout_degC { get; set; }

        public string FuelTempAtFlowMeter { get; set; }

        public string Miles_in_Seca_ { get; set; }

        public string ROB_Aux_lub_oil_ltr { get; set; }

        public string ROB_Cnk_lub_oil_ltr { get; set; }

        public string ROB_High_TBN_Cylinder_Oilltr { get; set; }

        public string ROB_LOW_TBN_Cylinder_Oilltr { get; set; }

        public string SBE_At_Berth { get; set; }

        public string Scavenge_air_pressure_mmWg { get; set; }

        public string ShaftPower { get; set; }

        public string AvgBHP { get; set; }

        public string SirePSC_Inspection { get; set; }

        public string Slops_Oil { get; set; }

        public string Slops_Water { get; set; }

        public string Stern_tube_Lub_lost_to_sea_ltr { get; set; }

        public string TChrgrRPM { get; set; }

        public string Torque { get; set; }

        public string Total_Bilge_water_tank_ROB_CubM { get; set; }

        public string Total_Bilge_water_tank_content__of_max { get; set; }

        public string Total_Sludge_tank_ROB_CubM { get; set; }

        public string Total_Sludge_tank_content__of_max { get; set; }

        public string WaterType { get; set; }

        public string Current_Mode_of_Scrubber { get; set; }

        public string Scrubber_in_Operation { get; set; }

        public string Avg_Cargo_Temp { get; set; }

        public string Aux_Boiler_Hrs { get; set; }

        public string Within_Ice_Edge { get; set; }

        public string Refuge_Port_Call { get; set; }

        //public List<CreateUpcomingPortDto> UpcomingPort { get; set; }

        public List<ImportRobDto> Robs { get; set; }

        //public List<EmAtSeaEventDto> EventSeaRobs { get; set; }

        //public List<EmInPortEventDto> EventPortRobs { get; set; }

        //public List<MEEventTypeDto> MeEventTypeRobs { get; set; }
        //public List<ImportFuelChangeoverDto> FuelChangeOver { get; set; }
        //   public List<ImportFuelChangeOverRobDto> FuelChangeOverROBCHECK { get; set; }
        public decimal? HoursCargoTankHeatingIncreased { get; set; }

        public decimal? HoursCargoTankHeatingMaintained { get; set; }

        public decimal? HoursCargoTanksCleaned { get; set; }

        public decimal? NumberofTanksCleaned { get; set; }

        public decimal? NumberofTanksHeated { get; set; }

        public decimal? NumberofTanksHeatingIncreased { get; set; }

        public string ECARegion { get; set; }

        public string BoilerHrs { get; set; }
        public string ReferencePort { get; set; }
        public string BaroMovement { get; set; }
        public string CurrentDirection { get; set; }
        public string DistilledWaterMade { get; set; }
        public string DistilledWaterCon { get; set; }
        public string DistilledWaterROB { get; set; }
        public string DistilledWaterProd { get; set; }
        public string SlopsMade { get; set; }
        public string Salinity { get; set; }
        public string Ballast { get; set; }
    }


    public class ImportRobDto
    {
        
        public string fuelType { get; set; }
        public string propulsion { get; set; }
        public string maneuver { get; set; }
        public string generator { get; set; }
        public string loaddischarge { get; set; }
        public string deballast { get; set; }
        public string idleon { get; set; }
        public string idleoff { get; set; }
        public string igs { get; set; }
        public string cargoheating { get; set; }
        public string cargoheatingplus { get; set; }
        public string cargoheatingplusplus { get; set; }
        public string cooling { get; set; }
        public string tankCleaning { get; set; }
        public string others { get; set; }
        public string pilotflame { get; set; }
        public string mainEngineConsumption { get; set; }
        public string auxEngineConsumption { get; set; }
        public string consumption { get; set; }
        public string boiler { get; set; }
        public string incinerator { get; set; }

        public string robstart { get; set; }

        public string robend { get; set; }
        public string remaining { get; set; }

        public string adjustment { get; set; }
        
    }

    //public class ImportFueltypeDto
    //{
    //    public string FuelType { get; set; }
    //    public string Propulsion { get; set; }
    //    public string Maneuver { get; set; }
    //    public string Generator { get; set; }
    //    public string Loaddischarge { get; set; }
    //    public string Deballast { get; set; }
    //    public string Idleon { get; set; }
    //    public string Idleoff { get; set; }
    //    public string Igs { get; set; }
    //    public string Cargoheating { get; set; }
    //    public string Cargoheatingplus { get; set; }
    //    public string Cargoheatingplusplus { get; set; }
    //    public string Cooling { get; set; }
    //    public string TankCleaning { get; set; }
    //    public string Others { get; set; }
    //    public string Pilotflame { get; set; }
    //    public string MainEngineConsumption { get; set; }
    //    public string AuxEngineConsumption { get; set; }
    //    public string Consumption { get; set; }
    //    public string Boiler { get; set; }
    //    public string Incinerator { get; set; }

    //    public string Robstart { get; set; }

    //    public string Robend { get; set; }
    //    public string Remaining { get; set; }

    //    public string Adjustment { get; set; }
    //}
}
