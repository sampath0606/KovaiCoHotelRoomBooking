import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Room } from '../Room';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { AuthenticationService } from './authentication.service';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from '@angular/router';
import { isUndefined, isNull, isNullOrUndefined } from 'util';

@Injectable()
export class BookingService {

  logs: Array<Room>;
  logSubject: BehaviorSubject<Array<Room>>;
  token: any;
  username:string;
  delvalue:boolean;
  


  constructor(private http: HttpClient, private authService: AuthenticationService,private router: Router) {
    this.logSubject = new BehaviorSubject(this.logs);
    this.token = this.authService.getBearerToken();
    this.username = this.authService.getUserName();
  }


  fetchNotesFromServer() {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    this.token = this.authService.getBearerToken();
    return this.http.get<Array<Room>>(`https://localhost:44387/api/Bookings/GetAllRoomInfo`,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    })
    .subscribe( notes => {
      this.logs = notes;
      this.logSubject.next(this.logs);
    },
    err => {
      return Observable.throw(err);
    });
  }

  checkAvailability( fromDate : string,toDate: string) {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    this.token = this.authService.getBearerToken();
    return this.http.get<Array<Room>>(`https://localhost:44387/api/Bookings/GetAllRoomInfo/${fromDate}/${toDate}`,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
    
  }

  fetchnotes() {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    this.token = this.authService.getBearerToken();
    return this.http.get<Array<Room>>(`https://localhost:44387/api/Bookings/GetAllRoomInfo`,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }



  getNotes(): BehaviorSubject<Array<Room>> {
    return this.logSubject;
  }

  clearNotes() {
     this.logs=[];
  }

  addNote(note: Room): Observable<Room> {
    note.AddedBy=this.username;
    this.token = this.authService.getBearerToken();
    return this.http.post<Room>(`https://localhost:44387/api/Bookings/AddRoom`, note, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    })
    .do (addNote => {
    this.fetchNotesFromServer();
      })
    .catch(err => {
      return Observable.throw(err);
    });
    
  }
  delteNote(noteitemId: string): Observable<Room> {
    this.token = this.authService.getBearerToken();
    return this.http.delete<Room>(`https://localhost:44387/api/Bookings/${noteitemId}`,{
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
     }
    ).do(
     delvalue => {
      // Update the edited notes by comparing the noteId
      if(delvalue){
        this.fetchNotesFromServer();
      }
    })
    .catch(err => {
      return Observable.throw(err);
    });
  }

  getNoteById(noteId): Room {
    
    // Get the note details by passing the noteId
    const noteValue = this.logs.find(note => note.RoomNo === noteId);
    return Object.assign({}, noteValue);
  }
}
