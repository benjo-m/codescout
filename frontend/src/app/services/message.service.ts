import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthService} from "./auth.service";
import {ActivatedRoute} from "@angular/router";
import {Message} from "../../models/message";

@Injectable({
  providedIn: 'root'
})
export class MessageService
{
  URL = 'https://localhost:7025/api';
  page :number = 0;

  constructor(private http: HttpClient, private authService:AuthService) { }



  isMessageFromSender(message: Message)
  {
    return message.senderId == this.authService.getUserId();
  }


  getMessages(receiverId:number, messageFromId: number | null = null, messageToId: number | null = null)
  {
    /*if(messageFromId && ((typeof messageFromId) == 'string')) messageFromId = new Date(messageFromId);
    if(messageToId && ((typeof messageToId) == 'string')) messageToId = new Date(messageToId);*/

    return this.http.get(`${this.URL}/Message/GetMessages?senderId=${this.authService.getUserId()}&receiverId=${receiverId}`
    + (messageFromId ? `&messageFromId=${messageFromId}` : ``)
    + (messageToId ? `&messageToId=${messageToId}` : ``));
  }



  getLatestMessages(receiverId:number)
  {
    return this.http.get(`${this.URL}/Message/GetRecentMessages?senderId=${this.authService.getUserId()}&receiverId=${receiverId}`);
  }



  getNewMessages(receiverId:number, messageToId: number | null)
  {
    //if(messageToId && ((typeof messageToId) == 'string')) messageToId = new Date(messageToId);

    return this.http.get(`${this.URL}/Message/GetNewMessages?senderId=${this.authService.getUserId()}&receiverId=${receiverId}`
    + (messageToId ? `&MessageToId=${messageToId}` : ``));
  }


  getLastMessageWithAllUsers()
  {
    return this.http.get(`${this.URL}/Message/GetLastMessageWithAllUsers?userId=${this.authService.getUserId()}`);
  }


  getNewMessageWithAllUsers()
  {
    return this.http.get(`${this.URL}/Message/GetNewMessageWithAllUsers?userId=${this.authService.getUserId()}`);
  }






  sendMessage(receiverId:number, text: string)
  {
    return this.http
      .post(
        `${this.URL}/Message/SendMessage?senderId=${this.authService.getUserId()}&receiverId=${receiverId}`,
        `"${text}"`,
        {headers: {'Content-Type' : 'application/json'}}
      );
  }
}
