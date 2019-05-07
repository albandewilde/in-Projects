import { GroupPeriod } from './GroupPeriod';

class PeriodCreate {
    public begDate! : Date 
    public endDate! : Date 
    public Kind! : String
    public Groups! : Array<GroupPeriod>
    public idZone! : Number

}
export { PeriodCreate }