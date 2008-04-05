COMPILER = gmcs
OUTDIR = bin

VPATH = $(OUTDIR)

all: ffmpeg-sharp.dll MWFVideoPlayer.exe

ffmpeg-sharp: ffmpeg-sharp.dll

ffmpeg-sharp.dll: src/*/*/*.cs src/*/*.cs src/*.cs
	mkdir -p $(OUTDIR)
	$(COMPILER) -debug -unsafe -target:library -out:$(OUTDIR)/$@ $^
	cp src/ffmpeg-sharp.dll.config $(OUTDIR)

MWFVIDEOPLAYER_FILES = examples/VideoPlayer/*.cs examples/VideoPlayer/Properties/*.cs

MWFVideoPlayer.exe: $(MWFVIDEOPLAYER_FILES) ffmpeg-sharp.dll
	$(COMPILER) -debug -out:$(OUTDIR)/$@ $(MWFVIDEOPLAYER_FILES) \
		-r:$(OUTDIR)/ffmpeg-sharp.dll -r:System.Drawing.dll \
		-r:System.Windows.Forms.dll

clean:
	rm -f $(OUTDIR)/ffmpeg-sharp.dll $(OUTDIR)/MWFVideoPlayer.exe \
		$(OUTDIR)/ffmpeg-sharp.dll.mdb $(OUTDIR)/MWFVideoPlayer.exe.mdb \
		$(OUTDIR)/ffmpeg-sharp.dll.config

.PHONY: all ffmpeg-sharp clean
