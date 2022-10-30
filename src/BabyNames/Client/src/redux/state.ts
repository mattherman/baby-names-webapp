import { IResultsState } from '~/results/results.slice';
import { ISessionState } from '~/session/session.slice';
import { IVotingState } from '~/voting/voting.slice';

export interface IState {
	voting: IVotingState;
	results: IResultsState;
	session: ISessionState;
}
