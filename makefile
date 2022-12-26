all: video

video: render
	ffmpeg -framerate 30 -i 'render_output/out_%05d.ppm' -c:v libx264 -crf 25 -vf "scale=1920:1080,format=yuv420p" -movflags +faststart render_output/video.mp4

render: build-release
	./RTIOW/bin/Release/net6.0/RTIOW

build-release:
	dotnet build --configuration Release

clean:
	rm render_output/*
