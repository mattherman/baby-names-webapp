import resultsSlice from '~/results/results.slice';
import sessionSlice from '~/session/session.slice';
import votingSlice from '~/voting/voting.slice';

export const rootReducer = {
	[votingSlice.name]: votingSlice.reducer,
	[resultsSlice.name]: resultsSlice.reducer,
	[sessionSlice.name]: sessionSlice.reducer,
};
