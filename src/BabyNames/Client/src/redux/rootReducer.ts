import votingSlice from '~/voting/voting.slice';

export const rootReducer = {
	[votingSlice.name]: votingSlice.reducer,
};
