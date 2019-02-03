using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Example.Droid
{
    public class CuteCircleProgressBar : View
    {
        private int mViewWidth;
        private int mViewHeight;

        private readonly float mStartAngle = -90;      // Always start from top (default is: "3 o'clock on a watch.")
        private float mSweepAngle = 0;              // How long to sweep from mStartAngle
        private float mMaxSweepAngle = 360;         // Max degrees to sweep = full circle
        private int mStrokeWidth = 20;              // Width of outline
        private int mAnimationDuration = 400;       // Animation duration for progress change
        private int mMaxProgress = 100;             // Max progress to use
        private bool mDrawText = true;           // Set to true if progress text should be drawn
        private bool mRoundedCorners = true;     // Set to true if rounded corners should be applied to outline ends
        private int mProgressColor = Color.Black;   // Outline color
        private int mTextColor = Color.Black;       // Progress text color

        private readonly Paint mPaint;                 // Allocate paint outside onDraw to avoid unnecessary object creation




        public CuteCircleProgressBar(Context context) :
       base(context)
        {
            mPaint = new Paint(PaintFlags.AntiAlias);
        }

        public CuteCircleProgressBar(Context context, IAttributeSet attrs) :
       this(context, attrs, Resource.Attribute.circularProgressBarStyle)
        {
        }

        public CuteCircleProgressBar(Context context, IAttributeSet attrs, int defStyle) :
          base(context, attrs, defStyle)
        {
            mPaint = new Paint(PaintFlags.AntiAlias);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            initMeasurments();
            drawOutlineArc(canvas);

            if (mDrawText)
            {
                drawText(canvas);
            }
        }

        private void initMeasurments()
        {
            mViewWidth = Width;
            mViewHeight = Height;
        }

        private void drawOutlineArc(Canvas canvas)
        {

            int diameter = Math.Min(mViewWidth, mViewHeight);
            float pad = mStrokeWidth / 2.0f;
            RectF outerOval = new RectF(pad, pad, diameter - pad, diameter - pad);

            mPaint.Color = new Color(mProgressColor);
            mPaint.StrokeWidth = mStrokeWidth;
            mPaint.AntiAlias = true;
            mPaint.StrokeCap = (mRoundedCorners ? Paint.Cap.Round : Paint.Cap.Butt);
            mPaint.SetStyle(Paint.Style.Stroke);
            canvas.DrawArc(outerOval, mStartAngle, mSweepAngle, false, mPaint);
        }

        private void drawText(Canvas canvas)
        {
            mPaint.TextSize = (Math.Min(mViewWidth, mViewHeight) / 5f);
            mPaint.TextAlign = Paint.Align.Center;
            mPaint.StrokeWidth = 0;
            mPaint.Color = new Color(mTextColor);

            // Center text
            int xPos = (canvas.Width / 2);
            int yPos = (int)((canvas.Height / 2) - ((mPaint.Descent() + mPaint.Ascent()) / 2));

            canvas.DrawText(calcProgressFromSweepAngle(mSweepAngle) + "%", xPos, yPos, mPaint);
        }

        private float calcSweepAngleFromProgress(int progress)
        {
            return (mMaxSweepAngle / mMaxProgress) * progress;
        }

        private int calcProgressFromSweepAngle(float sweepAngle)
        {
            return (int)((sweepAngle * mMaxProgress) / mMaxSweepAngle);
        }

        /**
         * Set progress of the circular progress bar.
         * @param progress progress between 0 and 100.
         */
        public void setProgress(int progress)
        {
            ValueAnimator animator = ValueAnimator.OfFloat(mSweepAngle, calcSweepAngleFromProgress(progress));
            animator.SetInterpolator(new DecelerateInterpolator());
            animator.SetDuration(mAnimationDuration);

            animator.Update += Animator_Update;
            animator.Start();
        }

        private void Animator_Update(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            mSweepAngle = (float)e.Animation.AnimatedValue;
            Invalidate();
        }

        public void setProgressColor(int color)
        {
            mProgressColor = color;
            Invalidate();
        }

        public void setProgressWidth(int width)
        {
            mStrokeWidth = width;
            Invalidate();
        }

        public void setTextColor(int color)
        {
            mTextColor = color;
            Invalidate();
        }

        public void showProgressText(bool show)
        {
            mDrawText = show;
            Invalidate();
        }

        /**
         * Toggle this if you don't want rounded corners on progress bar.
         * Default is true.
         * @param roundedCorners true if you want rounded corners of false otherwise.
         */
        public void useRoundedCorners(bool roundedCorners)
        {
            mRoundedCorners = roundedCorners;
            Invalidate();
        }
    }
}