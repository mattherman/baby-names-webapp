export enum NameGender {
	Male = 0,
	Female = 1,
}

export enum Vote {
	Nay = 0,
	Yea = 1,
}

export interface IBabyName {
	id: number;
	name: string;
	gender: NameGender;
	vote: Vote;
}
