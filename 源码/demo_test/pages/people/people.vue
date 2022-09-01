<template>
	<view>
		<!-- 图片 -->
		<view class="avatar">
			<image src="../../static/a.webp" @click="avatar"></image>
		</view>

		<view class="text">
			<table>
				<tr class="a">
					<td>
						<label style="font-size: 20px;">姓名: 彭昱畅</label>
					</td>
					<td>
						<input class="b" type="text" v-model="form.account" />
					</td>
				</tr>

				<tr class="c">
					<td>
						<label style="font-size: 20px;">院系: 软件工程学院</label>
					</td>
					<td>
						<input class="d" type="text" v-model="form.account" />
					</td>
				</tr>

				<tr class="e">
					<td>
						<label style="font-size: 20px;">专业: 软件技术</label>
					</td>
					<td>
						<input class="f" type="text" v-model="form.account" />
					</td>
				</tr>

				<tr class="g">
					<td>
						<label style="font-size: 20px;">班级: 软件二班</label>
					</td>
					<td>
						<input class="h" type="text" v-model="form.account" />
					</td>
				</tr>

				<tr class="i">
					<td>
						<label style="font-size: 20px;">学号: 2044010236</label>
					</td>
					<td>
						<input class="j" type="text" v-model="form.account" />
					</td>
				</tr>

				<tr class="k">
					<td>
						<label style="font-size: 20px;">电话: 145648213456</label>
					</td>
					<td>
						<input class="l" type="text" v-model="form.account" />
					</td>
				</tr>
			</table>
		</view>
		<image class="ing" src="../../static/ing.jpg"></image>
	</view>


</template>
<script>
	// 图片上传
	export default {
		data() {
			return {
				imgArr: ""
			}
		},
		methods: {
			chooseImg() {
				uni.chooseImage({
					// 上传图片张数
					count: 1,
					success: res => {
						this.imgArr = res.tempFilePaths
					}
				})
			}
		}
	}
</script>

<style>
	/* 底部图片 */
	.ing {
		margin-left: -1px;
		margin-top: 350px;
		width: 100%;
		height: 500px;
	}

	// 头像背景设置
	.avatar {
		background-color: #4169E1;
		width: 100%;
		height: 200px;
	}

	// 头像
	.avatar>image {
		border-radius: 50%;
		height: 100px;
		width: 100px;
		margin-left: 35%;
		margin-top: 50px;
	}

	/* 上传头像 */
	.button {
		margin-top: 160px;
		margin-left: 30px;
		width: 100px;
		height: 30px;
	}

	/* 图片 */
	image {
		margin-top: -2px;
		margin-left: 30px;
		width: 100px;
		height: 100px;
	}

	.a {
		position: absolute;
		top: 220px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
		size: 20rpx;
	}

	.b {
		position: absolute;
		top: 240px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.c {

		position: absolute;
		top: 260px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.d {
		position: absolute;
		top: 280px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.e {

		position: absolute;
		top: 300px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.f {
		position: absolute;
		top: 320px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;

	}

	.g {

		position: absolute;
		top: 340px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.h {
		position: absolute;
		top: 360px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.i {

		position: absolute;
		top: 380px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.j {
		position: absolute;
		top: 400px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;

	}

	.k {

		position: absolute;
		top: 420px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;

	}

	.l {
		position: absolute;
		top: 460px;
		left: 20px;
		width: 150px;
		margin-left: 25px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;

	}

	.text {
		margin-left: 100px;
	}
</style>
