import { IResultsState } from '~/results/results.slice';
import { IVotingState } from '~/voting/voting.slice';

export interface IState {
	voting: IVotingState;
	results: IResultsState;
}
