using System;

using FFmpegSharp.Interop;
using FFmpegSharp.Interop.Format;
using FFmpegSharp.Interop.Codec;
using FFmpegSharp.Interop.Util;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using FFmpegSharp;
using FFmpegSharp.Interop.SWScale;

namespace FFmpegTest
{
	public unsafe class VideoStream
	{

		//private FFmpeg.PixelFormat PixFmt = FFmpeg.PixelFormat.PIX_FMT_RGB24;
		//private PixelFormat PixFmt = PixelFormat.PIX_FMT_YUV420P;
		private PixelFormat PixFmt = PixelFormat.PIX_FMT_RGB24;

		//private IntPtr pFormatContext;
		private AVFormatContext FormatContext;

		//private IntPtr pVideoCodecContext;
		private AVCodecContext videoCodecContext;

		private int videoStartIndex = -1;
		private int width = 0;
		private int height = 0;

		//private IntPtr pVideoStream;

		//private IntPtr pVideoCodec;

		private IntPtr pFrame;
		private AVFrame Frame;

		//private IntPtr pPicture;
		private AVPicture Picture;

		private SwsContext _swsContext;

		//private IntPtr pPacket;
		private AVPacket Packet;
		private byte[] buffer;

		public VideoStream()
		{
			FFmpeg.av_register_all();
		}

		public unsafe bool Open(string path)
		{
			Reset();

			if (FFmpeg.av_open_input_file(out FormatContext, path) != 0)
			{
				Console.WriteLine("Couldn't open input file.");
				return false;
			}
			if (FFmpeg.av_find_stream_info(ref FormatContext) < 0)
			{
				Console.WriteLine("Couldn't find stream information.");
				return false;
			}

			//FormatContext = (AVFormatContext) Marshal.PtrToStructure(pFormatContext, typeof(AVFormatContext));
			for (int i = 0; i < FormatContext.nb_streams; i++)
			{
				//AVStream stream = (AVStream) Marshal.PtrToStructure(FormatContext.streams[i], typeof(AVStream));
				AVStream* stream = (AVStream*) FormatContext.streams[i];
				//AVCodecContext codec = (AVCodecContext) Marshal.PtrToStructure(new IntPtr(stream->codec), typeof(AVCodecContext));
				AVCodecContext codec = *stream->codec;
				//Find the first video stream.
				if ((codec.codec_type == CodecType.CODEC_TYPE_VIDEO) && (videoStartIndex == -1))
				{
					//pVideoCodecContext = new IntPtr(stream->codec);
					videoCodecContext = codec;
					//pVideoStream = new IntPtr(FormatContext.streams[i]);
					//videoCodecContext = codec;
					videoStartIndex = i;

					AVCodec* pVideoCodec = FFmpeg.avcodec_find_decoder(videoCodecContext.codec_id);
					//Find the decoder for the video stream
					if (FFmpeg.avcodec_open(ref videoCodecContext, ref *pVideoCodec) < 0)
					{
						Console.WriteLine("Couldn't open Codec.");
						return false;
					}
				}
			}
			if (videoStartIndex == -1)
			{
				Console.WriteLine("Couldn't find Video Stream.");
				return false;
			}
			return true;
		}

		private void SetUpFrame()
		{
			//Frame = (AVFrame) Marshal.PtrToStructure(FFmpeg.avcodec_alloc_frame(), typeof(AVFrame));
			pFrame = new IntPtr(FFmpeg.avcodec_alloc_frame());
			Frame = (AVFrame) Marshal.PtrToStructure(pFrame, typeof(AVFrame));

			width = videoCodecContext.width;
			height = videoCodecContext.height;

			int numBytes = FFmpeg.avpicture_get_size(PixFmt, width, height);
			buffer = new byte[numBytes];

			Picture = new AVPicture();
			FFmpeg.avpicture_alloc(ref Picture, (int) PixFmt, width, height);
			//pPicture = Marshal.AllocHGlobal(Marshal.SizeOf((object) (new AVPicture())));
			//FFmpeg.avpicture_alloc(pPicture, (int) PixFmt, width, height);

			//pPacket = Marshal.AllocHGlobal(Marshal.SizeOf(new AVPacket()));

			_swsContext = *FFmpeg.sws_getContext(videoCodecContext.width, videoCodecContext.height, (int) videoCodecContext.pix_fmt,
				videoCodecContext.width, videoCodecContext.height, (int) PixFmt, FFmpeg.SWS_BICUBIC);
		}

		private unsafe void ReadFrame()
		{
			Packet = new AVPacket();
			FFmpeg.av_init_packet(ref Packet);

			bool frameFinished = false;
			int byteCount;

			int count = 0;
			while (FFmpeg.av_read_frame(ref FormatContext, ref Packet) >= 0)
			{
				
				
				
				
				pFrame = new IntPtr(FFmpeg.avcodec_alloc_frame());
				Frame = (AVFrame) Marshal.PtrToStructure(pFrame, typeof(AVFrame));

				/*width = videoCodecContext.width;
				height = videoCodecContext.height;

				int numBytes = FFmpeg.avpicture_get_size(PixFmt, width, height);
				buffer = new byte[numBytes];

				Picture = new AVPicture();
				FFmpeg.avpicture_alloc(ref Picture, (int) PixFmt, width, height);
				//pPicture = Marshal.AllocHGlobal(Marshal.SizeOf((object) (new AVPicture())));
				//FFmpeg.avpicture_alloc(pPicture, (int) PixFmt, width, height);

				//pPacket = Marshal.AllocHGlobal(Marshal.SizeOf(new AVPacket()));

				_swsContext = *FFmpeg.sws_getContext(videoCodecContext.width, videoCodecContext.height, (int) videoCodecContext.pix_fmt,
					videoCodecContext.width, videoCodecContext.height, (int) PixFmt, FFmpeg.SWS_BICUBIC);*/





				//Packet = (AVPacket) Marshal.PtrToStructure(pPacket, typeof(AVPacket));

				if (Packet.stream_index == videoStartIndex)
				{
					byteCount = FFmpeg.avcodec_decode_video(ref videoCodecContext,
						pFrame, out frameFinished, Packet.data, Packet.size);
					if (byteCount < 0)
					{
						Console.WriteLine("Couldn't decode frame.");
					}

					if (frameFinished)
					{
						//int res = FFmpeg.img_convert(pPicture, PixFmt, pFrame, videoCodecContext.pix_fmt, width, height);
						/*AVPicture typedFrame = new AVPicture();
						IntPtr tempPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typedFrame));
						Marshal.StructureToPtr(Frame, tempPtr, false);
						typedFrame = (AVPicture) Marshal.PtrToStructure(tempPtr, typeof(AVPicture));*/

						Frame = (AVFrame) Marshal.PtrToStructure(pFrame, typeof(AVFrame));
						//int res = FFmpeg.img_convert(ref Picture, PixFmt, ref Frame, videoCodecContext.pix_fmt, width, height);

						fixed (int* sourceData = Frame.data, sourceLinesize = Frame.linesize, destinationData = Picture.data, destinationLinesize = Picture.linesize)
						{
							FFmpeg.sws_scale(ref _swsContext, sourceData, sourceLinesize, 0, videoCodecContext.height, destinationData, destinationLinesize);
						}

						// TESTING
						/*const int W = 96;
						const int H = 96;

						byte[] rgb_data = new byte[W * H * 4];
						byte*[] rgb_src;
						fixed (byte* rgb_data_ptr = rgb_data)
						{
							rgb_src = new byte*[] { rgb_data_ptr, null, null };

							int[] rgb_stride = new int[] { 4 * W, 0, 0 };
							byte[] data = new byte[3 * W * H];
							byte*[] src;
							fixed (byte* data_ptr = data)
							{
								src = new byte*[] { data_ptr, data_ptr + W * H, data_ptr + W * H * 2 };

								int[] stride = new int[] { W, W, W };

								SwsContext* sws = FFmpeg.sws_getContext(W / 12, H / 12, (int) PixelFormat.PIX_FMT_RGB32, W, H, (int) PixelFormat.PIX_FMT_YUV420P, 2);

								Random random = new Random(Environment.TickCount);
								for (int y = 0; y < H; y++)
									for (int x = 0; x < W * 4; x++)
										rgb_data[x + y * 4 * W] = (byte) random.Next(byte.MaxValue);

								FFmpeg.sws_rgb2rgb_init(0);
								FFmpeg.sws_scale(sws, rgb_src, rgb_stride, 0, H, src, stride);
							}
						}*/

						//int res = FFmpeg.img_convert(ref Picture, PixFmt, pFrame, videoCodecContext.pix_fmt, width, height);
						//AVPicture picture = (AVPicture) Marshal.PtrToStructure(pPicture, typeof(AVPicture));
					/*	fixed (int* pictureData = Picture.data)
						{
							Marshal.Copy(new IntPtr(pictureData[0]), buffer, 0, buffer.Length);
						}*/

						//ViewVideo();
						if (count < 5)
							SaveFrame(count);
						count++;
					}
				}

				//FFmpeg.avpicture_free(ref Picture);
				FFmpeg.av_free(ref pFrame);
				//FFmpeg.av_free_packet(ref Packet);
			}

			FFmpeg.avcodec_close(ref videoCodecContext);
			FFmpeg.av_close_input_file(ref FormatContext);
		}

		private void Reset()
		{
			//if (new IntPtr(FormatContext) != IntPtr.Zero)
			//{
			try
			{
				FFmpeg.av_close_input_file(ref FormatContext);
			}
			catch { }
			//}
			videoStartIndex = -1;
		}

		public void RunVideo()
		{
			SetUpFrame();
			ReadFrame();
		}

		private void SaveFrame(int count)
		{
			string header = "P6\n" + width + " " + height + "\n" + "255\n";
			FileStream fs = new FileStream("out" + count + ".ppm", FileMode.Create);
			BinaryWriter w = new BinaryWriter(fs);
			w.Write(Encoding.UTF8.GetBytes(header));
			w.Write(buffer);
			w.Close();
			fs.Close();
		}
	}
}
