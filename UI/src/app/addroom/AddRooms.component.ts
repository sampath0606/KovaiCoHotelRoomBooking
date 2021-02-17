// import { Checklist, ChecklistdialogComponent } from './../checklistdialog/checklistdialog.component';
import { Component, TemplateRef } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes'
import { Room } from '../Room';
import { BookingService } from '../services/notes.service';
import { MatChipInputEvent, MatSnackBar } from '@angular/material';
import { RouterService } from '../services/router.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { AuthenticationService } from '../services/authentication.service';

export interface Label {
  id: Number;
  name: string;
}



@Component({
  selector: 'app-ChangeLog',
  templateUrl: './AddRooms.component.html',
  styleUrls: ['./AddRooms.component.css']
})


export class AddRoomsComponent {
  errMessage: string;
  room: Room = new Room();
  rooms: Array<Room> = [];
  roomTypes: Label[] = [{ id: 1, name: "Single" }, { id: 2, name: "Double" }];

  // checklists:Checklist[];
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  modalRef: BsModalRef;
  constructor(private BookingService: BookingService, private routerService: RouterService, private modalService: BsModalService,
    private authservice: AuthenticationService,public snackbar : MatSnackBar) {
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  closemodal(event) {
    if (this.modalRef) {
      this.modalRef.hide();
    }
  }

  addlogs() {

    if (this.room.Cost == 0 || this.room.Occupancy == 0 || this.room.RoomNo.trim() === '' || this.room.Type == '') {
      // add the error message when fields are empty
      this.errMessage = 'Invlid Inputs, Please fill in all the fields';
    } else {

      this.room.AddedBy = this.authservice.getUserName();
      this.room.RoomNo.toUpperCase();
      this.BookingService.addNote(this.room).subscribe(
        data => {
          this.errMessage = '';
          this.snackbar.open("Room Added","X");
          this.routerService.routeToListView();
        },
        err => {
          if(err.status==499)
          {
            this.snackbar.open("Room with the Same RoomNo Exists","X");
            // this.errMessage = "Room with the Same RoomNo Exists";
          }
          else
          {
          this.errMessage = err.message;
          }
        }
      );
    }
    this.room = new Room();
  }


}
