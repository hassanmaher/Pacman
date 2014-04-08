using System;
using System.IO;
using Microsoft.DirectX.DirectSound;
using Buffer = Microsoft.DirectX.DirectSound.Buffer;

namespace Chomp
{
	public class GameSound
	{
		private Buffer3D _buffer3D;
		private Device _soundDevice;
		private BufferDescription _bufferDesc;
		private SecondaryBuffer _soundBuffer;

		public GameSound(System.Windows.Forms.Control Owner)
		{
			_bufferDesc = new BufferDescription();
			_bufferDesc.GlobalFocus = true;
			_bufferDesc.Control3D = true;
			_soundDevice = new Device();
			_soundDevice.SetCooperativeLevel(Owner, CooperativeLevel.Normal);
		}

		public void PlaySound(Stream EmbeddedSoundStream, bool Loop)
		{
			try
			{
				_soundBuffer = new SecondaryBuffer(EmbeddedSoundStream, _bufferDesc, _soundDevice);
				_buffer3D = new Buffer3D(_soundBuffer);
				if (Loop)
				{
					_soundBuffer.Play(0, BufferPlayFlags.Looping);
				}
				else
				{
				
					_soundBuffer.Play(0, BufferPlayFlags.Default);
				}
			}
			catch (Exception expPlay)
			{
				Console.WriteLine(expPlay.Message);
			}
		}

		public void PlaySound(string FullResourceName, bool Loop)
		{
			try
			{
				Stream strmTemp = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(FullResourceName);
				_soundBuffer = new SecondaryBuffer(strmTemp, _bufferDesc, _soundDevice);
				_buffer3D = new Buffer3D(_soundBuffer);
				if (Loop)
				{
					_soundBuffer.Play(0, BufferPlayFlags.Looping);
				}
				else
				{
				
					_soundBuffer.Play(0, BufferPlayFlags.Default);
				}
			}
			catch (Exception expPlay)
			{
				Console.WriteLine(expPlay.Message);
			}
		}

		public void StopSound()
		{
			if (_soundBuffer != null) _soundBuffer.Stop();
		}

		public static Stream GetEmbeddedSoundStream(string FullResourceName)
		{
			try
			{
				Stream strmTemp = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(FullResourceName);
				return strmTemp;
			}
			catch (Exception ex)
			{
				throw new Exception("Could not retrieve embedded resource. " + ex.Message);
			}
		}
	}
}
