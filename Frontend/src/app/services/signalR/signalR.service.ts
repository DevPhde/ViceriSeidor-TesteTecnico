import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { InteractionService } from '../interaction/interaction.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection | undefined;
  constructor(private interactionService: InteractionService) {
    this.hubConnection?.onclose(error => {
      this.retryConnection();
    });
  }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:6037/heroesHub", { skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets })
      .configureLogging(signalR.LogLevel.Information)
      .build();
    this.hubConnection
      .start()
      .then(() => {
        this.getHeroesOnInit();
        this.updateHeroes();
      })
      .catch(err => {
        this.retryConnection();
      });

    this.hubConnection.onclose(() => {
      console.log('Connection closed');
      this.retryConnection();
    });
  }

  getHeroesOnInit() {
    this.hubConnection?.invoke("SendHeroes")
      .then(data => { })
  }
  updateHeroes() {
    this.hubConnection?.on("SendHeroes", (heroes) => {
      this.interactionService.setHeroes(heroes);
    })
  }


  private reconnectInterval = 5000;
  private maxRetries = 10;
  private retryCount = 0;

  private retryConnection(): void {
    if (this.retryCount < this.maxRetries) {
      setTimeout(() => {
        this.retryCount++;
        console.log(`Attempting to reconnect... (${this.retryCount}/${this.maxRetries})`);
        this.startConnection();
      }, this.reconnectInterval);
    } else {
      console.log('Max retries reached. Giving up.');
    }
  }
}
