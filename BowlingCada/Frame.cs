using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingCada
{
	public class Game
	{
		public Frame Head { get; set; }
		public Frame Tail { get; set; }

		public Frame AddFrame(int? score1, int? score2)
		{
			if (Head == null)
			{
				Frame f = new Frame()
				{
					FrameNo = 1,
					Score1 = score1,
					Score2 = score2,
					Previous = Tail
				};
				Head = f;

				return f;
			}
			else
			{
				if (Tail != null)
				{
					Frame f = Tail.Add(score1, score2);
					Tail = f;

				}
				else
				{
					Frame f = Head.Add(score1, score2);
					Tail = f;
				}
				return Tail;
			}

		}
	}
	
	public class Frame
	{
		public int FrameNo { get; set; }
		public int? Score1 { get; set; }
		public int? Score2 { get; set; }
		public int? Total { get { return GetFrameTotal(); } }

		protected virtual int? GetFrameTotal()
		{
			if (IsStrike())
			{
				return (Score1 ?? 0) + (GetNextTwoRolls() ?? 0);
			}

			if (IsSpare())
			{
				return (Score1 ?? 0) + (Score2 ?? 0) + (GetNextOneRoll() ?? 0);
			}
			
			return (Score1 ?? 0) + (Score2 ?? 0);
		}

		private int? GetNextTwoRolls()
		{
			if (Next == null)
			{
				return 0;
			}

			if (Next.IsStrike())
			{
				return (Next.Score1 ?? 0) + (Next.Next == null ? 0 : (Next.Next.Score1 ?? 0));
			}

			return (Next.Score1 ?? 0) + (Next.Score2 ?? 0);
		}

		private int? GetNextOneRoll()
		{
			return (Next != null ? (Next.Score1 ?? 0) : 0);
		}

		public int GetGrandTotal()
		{
			if (Previous != null)
			{
				return Previous.GetGrandTotal() + (GetFrameTotal() ?? 0);
			}

			return (GetFrameTotal() ?? 0);
		}

		protected bool IsStrike()
		{
			return Score1 == 10;
		}

		protected bool IsSpare()
		{
			return !IsStrike() && ((Score1 ?? 0) + (Score2 ?? 0)) == 10;
		}

		public Frame Add(int? score1, int? score2)
		{
			var newFrame = new Frame
			               	{
			               		Score1 = score1,
								Score2 = score2,
								Previous = this,
								FrameNo = this.FrameNo + 1
			               	};
			Next = newFrame;
			return newFrame;
		}

		public Frame Next { get; set; }
		public Frame Previous { get; set; }
	}

	public class LastFrame : Frame
	{
		public LastFrame()
		{
			FrameNo = 10;
		}

		public int? Score3 { get; set; }

		protected override int? GetFrameTotal()
		{
			if (IsStrike() || IsSpare())
			{
				return (Score1 ?? 0) + (Score2 ?? 0) + (Score3 ?? 0);
			}
			return base.GetFrameTotal();
		}
	}
}
