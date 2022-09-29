const path = require('path');

const BUILD_DIRECTORY = path.join('..', 'wwwroot', 'scripts');
const BUILD_DIRECTORY_ABSOLUTE = path.resolve(__dirname, BUILD_DIRECTORY);

module.exports = {
	entry: './src/index.js',
	output: {
		filename: 'main.js',
		path: BUILD_DIRECTORY_ABSOLUTE,
		publicPath: '/scripts/',
	},
};
