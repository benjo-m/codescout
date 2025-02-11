export interface Message {
  id: number;
  text: string;
  dateSent: Date;
  deleted: boolean;
  senderId: number;
}
