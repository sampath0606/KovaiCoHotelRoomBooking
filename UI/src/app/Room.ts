
export class Room {
  RoomNo: string;
  Type: string;
  Cost: number;
  Occupancy: number;
  Reserved: Reserved[];
  AddedBy: string;
  Status: string;
  
  constructor() {
    this.RoomNo='';
    this.Type ='';
    this.Cost = 0;
    this.Reserved = [];
    this.AddedBy = '';
  }
}

export class Reserved{
  FromDate : Date;
  ToDate : Date ;
  BookedUserMail : string;

}
