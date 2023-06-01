import { EventEmitter, Injectable } from '@angular/core';
import { calculateRating } from '../interfaces/calculateRating';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  defaultPath:string = "https://localhost:7179/hubs/calculateRating";
  private hubConnection: HubConnection;
  newRating = new Subject<calculateRating>();
  constructor() {
    this.hubConnection = {} as HubConnection
   }
  startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.defaultPath,{
        skipNegotiation:true,
        transport:HttpTransportType.WebSockets
      }) 
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('SignalR connection started.');
      })
      .catch(err => console.error('Error while starting SignalR connection:', err));
  
    }
  closeConnection():void{
    this.hubConnection.stop().then(()=>{
      console.log("closed connection");
      
    })
  }
  UpdateRating(movieId:number){
    this.hubConnection.invoke("UpdateRating",movieId)
        .catch(err=>console.log(err));
  }

  avgRating = new EventEmitter<number>();
  movieId = new EventEmitter<number>();
  raiseAvgRatingEmmiter(avgRating:number){
    // console.log(avgRating)
    if(avgRating != undefined){
      this.avgRating.emit(avgRating)

    }
  }
  raiseMovieIdEmmiter(id:number){
    this.movieId.emit(id);
  }


  newMovieRatingListener(){
    this.hubConnection.on("avgMovieRating",(res)=>{
    this.raiseAvgRatingEmmiter(res.avgRating);
    this.raiseMovieIdEmmiter(res.id);
    })
  }
    

    
}
    
  
  
  
