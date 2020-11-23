import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http:HttpClient) {}

  private addMessagePath = environment.messagesApiUrl + 'message/add'
  
  addMessage(data): Observable<boolean>{
    return this.http.post<boolean>(this.addMessagePath, data)
  }
}
