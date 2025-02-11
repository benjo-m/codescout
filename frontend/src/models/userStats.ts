export interface UserStats {
  projectsSubmitted: number;
  awardsUnlocked: number;
  technologiesWorkedWith: number;
  topTechnologies: {
    [technology: string]: number;
  };
}
