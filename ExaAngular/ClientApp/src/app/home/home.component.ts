import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from "@angular/forms";





@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  topics: String[] = [];

  public Feedbacks: FeedbackModel = {
    name: "",
    email: "",
    telephone: "",
    topic: "",
    message: "",
  };
  public telephone = "^((\\+7|7|8)+([0-9]){10})$";

  constructor(public http: HttpClient) {
    
    this.http.get<String[]>('https://localhost:44450/' + 'home').subscribe(result => {

      this.topics = result;

    });
    
  }

  selTopic: string = "";

  public Send() {

    const bodyFeedback = {
      name: this.Feedbacks.name,
      email: this.Feedbacks.email,
      telephone: this.Feedbacks.telephone,
      topic: this.selTopic,
      message: this.Feedbacks.message,
    };
    

    this.http.post<FeedbackModel>('https://localhost:44450/' + 'home/', bodyFeedback).subscribe(result2 => {

      this.Feedbacks = result2;

    });

  }

  public selectTopic(event: any) {
    this.selTopic = event.target.value;
  }

}



interface FeedbackModel {
  name: string;
  email: string;
  telephone: string;
  topic: string;
  message: string;

}
