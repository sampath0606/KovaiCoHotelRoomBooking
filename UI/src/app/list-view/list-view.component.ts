import { Component, OnInit, ViewChild } from '@angular/core';
import {  Room } from '../Room';
import { BookingService } from '../services/notes.service';
import { MatTableDataSource, MatTable, MatPaginator, Sort, MatSnackBar, MatSnackBarConfig } from '@angular/material';
import { DatePipe } from '@angular/common';


export class BookingDates{
  fromDate = new Date();
  toDate = new Date();
}


@Component({
  selector: 'app-list-view',
  templateUrl: './list-view.component.html',
  styleUrls: ['./list-view.component.css']
})
export class ListViewComponent implements OnInit {

  displayedColumns: string[] = ['RoomNo', 'Type', 'Status'];
  rooms: Room[] = [];
  room: Room;
  dataSource: MatTableDataSource<Room>;
  @ViewChild(MatTable) table: MatTable<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  bookedDates : BookingDates =  {fromDate: new Date(),toDate: new Date()};

  minDate = new Date();
  maxDate = new Date();





  constructor(private BookingService: BookingService,public datepipe: DatePipe,public snackbar : MatSnackBar) {
    
  this.maxDate.setDate( this.maxDate.getDate() + 60 );
  }

  ngOnInit() {
    // this.BookingService.fetchnotes().subscribe(
    //   response => {
    //     this.rooms = response;
    //     console.log(this.rooms);
    //     this.setupDatasource(this.rooms);
    //   });
  };

  getRoomsAvailability(){

    if(this.checkDate()){
    // alert(this.datepipe.transform(this.bookedDates.fromDate, 'yyyy-MM-dd')+ "/" +this.datepipe.transform(this.bookedDates.toDate, 'yyyy-MM-dd'));
    this.BookingService.checkAvailability(
      this.datepipe.transform(this.bookedDates.fromDate, 'yyyy-MM-dd'),
      this.datepipe.transform(this.bookedDates.toDate, 'yyyy-MM-dd')).subscribe(
      response=>{
        this.rooms=response;
        this.setupDatasource(this.rooms);
        console.log(this.rooms);
      }
      
    )
    }
  }

  checkDate()
  {

     if ( this.bookedDates.fromDate.getTime()  >this.bookedDates.toDate.getTime())
    {

      let config = new MatSnackBarConfig();
      config.verticalPosition = "top";
      this.snackbar.open("Invalid Selection for the Availability Check","X",config);
      return false;
    }
    else
    {
      return true;
    }
  }

  setupDatasource(data: any) {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.paginator = this.paginator;
    this.table.renderRows();
  }

  removeAt(index) {
    const data = this.dataSource.data;
    this.room = this.dataSource.data[index]; //.Number;
    var itemidtoremove = this.room.RoomNo;
    this.BookingService.delteNote(this.room.RoomNo).subscribe(
      x => {
        console.log("Item Deleted");
        data.splice((this.paginator.pageIndex * this.paginator.pageSize) + index, 1);
        this.rooms = data;
        this.setupDatasource(data);
      },
      err => {
        console.log("Something went wrong, please refresh");
        this.setupDatasource(data);
      }
    )

  }

  getbadgecolor(type: string) {
    if (type === 'Single') {
      return 'badge-Single';
    }
    if (type === 'Double') {
      return 'badge-Double';
    }
  
}

getbadgecolorforstatus(type: string) {
  if (type === 'Available') {
    return 'badge-Available';
  }
  if (type === 'Booked') {
    return 'badge-Booked';
  }

}

// roomTypes:Label[]= [{id:1,name:"Economy"},{id:2,name:"Deluxe"},{id:3,name:"Luxury"}];

}

