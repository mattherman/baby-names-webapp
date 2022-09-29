const path = require('path');
const autoprefixer = require('autoprefixer');

const BUILD_DIRECTORY = path.join('..', 'wwwroot', 'scripts');
const BUILD_DIRECTORY_ABSOLUTE = path.resolve(__dirname, BUILD_DIRECTORY);

const IS_DEVELOPMENT = process.argv.includes('development');

const postCSSLoader = {
	loader: 'postcss-loader',
	options: {
		postcssOptions: {
			plugins: [autoprefixer()]
		}
	}
};

module.exports = {
	entry: './src/index.tsx',
	mode: IS_DEVELOPMENT ? 'development' : 'production',
	module: {
		rules: [
			{
				test: /\.(ts|tsx)$/,
				loader: 'babel-loader',
				options: { presets: ['@babel/preset-typescript'] }
			},
			{
				test: /\.(js|jsx)$/,
				loader: 'babel-loader',
				options: { presets: ['@babel/env'] }
			},
			{
				test: /\.css$/,
				use: ['style-loader', 'css-loader', postCSSLoader]
			}
		],
	},
	resolve: { extensions: ['*', '.js', '.jsx', '.ts', '.tsx'] },
	output: {
		filename: 'main.js',
		path: BUILD_DIRECTORY_ABSOLUTE,
		publicPath: '/scripts/',
	}
};
