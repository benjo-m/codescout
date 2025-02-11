import { Message } from '../../../models/message';

export interface UserMessagesResponse {
  userId: number;
  username: string;
  messages: Message[];
}
