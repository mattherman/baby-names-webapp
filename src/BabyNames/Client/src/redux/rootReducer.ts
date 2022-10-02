import namesSlice from '~/names/names.slice';

export const rootReducer = {
	[namesSlice.name]: namesSlice.reducer,
};
