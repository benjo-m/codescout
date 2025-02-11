import { Component, OnInit, numberAttribute } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { User } from 'src/models/user';
import { ProjectService } from '../../../services/project.service';
import { SubmittedProject } from 'src/models/submittedProject';
import { UserStats } from 'src/models/userStats';
import { ActivatedRoute } from '@angular/router';
import { FriendService } from '../../../services/friend.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  userId: number = 0;
  user: User | null = null;
  projects: SubmittedProject[] = [];
  userStats: UserStats | null = null;

  isCurrentUser: boolean = true;
  isFriend: boolean | null = null;

  socials = 1;

  routeUserId: string | null = null;

  awardImagesPath = '../../assets/images/awards/';

  constructor(
    private authService: AuthService,
    private projectService: ProjectService,
    private friendService: FriendService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.routeUserId = this.route.snapshot.paramMap.get('id');

    if (this.routeUserId && +this.routeUserId != this.authService.getUserId()) {
      this.userId = +this.routeUserId;
      this.isCurrentUser = false;

      this.friendService.isFriend(this.userId).subscribe((value) => {
        this.isFriend = value;
      });
    } else this.userId = this.authService.getUserId();

    this.authService.getUser(this.userId).subscribe((data) => {
      this.user = data;
    });

    this.projectService
      .getProjectsByUser(this.userId)
      .subscribe((data: SubmittedProject[]) => (this.projects = data));

    this.authService.getUserStats(this.userId).subscribe((data: UserStats) => {
      this.userStats = data;
    });
  }

  isRecruiter() {
    if (this.user) return this.authService.isUserRecruiter(this.user);
    else return false;
  }

  sortedTechnologies() {
    if (this.userStats) {
      return Object.entries(this.userStats.topTechnologies).sort(
        (a, b) => b[1] - a[1]
      );
    }

    return [];
  }

  btnFriend() {
    if (!this.isWaitingFriendRequestResponse()) {
      if (this.isFriend) {
        this.friendService.removeFriend(this.userId).subscribe((value) => {
          this.isFriend = null;
        });
      } else if (this.isFriend == null) {
        this.friendService.sendFriendRequest(this.userId).subscribe((value) => {
          this.isFriend = false;
        });
      }
    }
  }

  isWaitingFriendRequestResponse() {
    return this.isFriend != null && this.isFriend == false;
  }

  isSocialsEmpty(): boolean {
    return (
      !this.user?.socials.gitHub &&
      !this.user?.socials.stackOverflow &&
      !this.user?.socials.linkedIn &&
      !this.user?.socials.medium &&
      !this.user?.socials.x
    );
  }
}
