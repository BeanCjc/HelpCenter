<template>
    <div class="doc-editor">
        <!-- 图片上传组件辅助-->
        <el-upload
            class="avatar-uploader"
            :action="serverUrl"
            name="img"
            :headers="header"
            :show-file-list="false"
            :on-success="uploadSuccess"
            :on-error="uploadError"
            :before-upload="beforeUpload">
        </el-upload>
        <!--富文本编辑器组件-->
        <el-row v-loading="uillUpdateImg">
            <!--<quill-editor-->
                <!--v-model="detailContent"-->
                <!--ref="myQuillEditor"-->
                <!--:options="editorOption"-->
                <!--@blur="onEditorBlur($event)"-->
                <!--@focus="onEditorFocus($event)"-->
                <!--@change="onEditorChange($event)"-->
                <!--@ready="onEditorReady($event)"-->

            <!--&gt;-->
            <quill-editor
                v-model="detailContent"
                ref="myQuillEditor"
                :options="editorOption"
                @change="onEditorChange($event)"
                @ready="onEditorReady($event)"

            >
            </quill-editor>
        </el-row>
    </div>
</template>

<script>
    import 'quill/dist/quill.core.css';
    import 'quill/dist/quill.snow.css';
    import 'quill/dist/quill.bubble.css';
    import {quillEditor} from 'vue-quill-editor';
    import '../../common/image-paste.min.js';
//
//    import Bus from '../common/js/bus.js'


    // 工具栏配置
    const toolbarOptions = [
        ['bold', 'italic', 'underline'],        // toggled buttons
        [{'header': 1}, {'header': 2}],
        [{'list': 'ordered'}, {'list': 'bullet'}],
        [{'size': ['small', false, 'large', 'huge']}],
        [{'header': [1, 2, 3, 4, 5, 6, false]}],
        ['link', 'image'],
        [{ 'indent': '-1'}, { 'indent': '+1' }]
    ]
    export default {
        name: 'docEditor',
        props: {
            docContent:''
        },
        data: function () {
            return {
                quillUpdateImg: false, // 根据图片上传状态来确定是否显示loading动画，刚开始是false,不显示
                serverUrl: 'api/File',  // 这里写你要上传的图片服务器地址
                header: {token: sessionStorage.token},  // 有的图片服务器要求请求头需要有token之类的参数，写在这里
                detailContent: '', // 富文本内容
                content: '',
                editorOption: {
                    placeholder: 'Hello World',
                    modules: {
                        toolbar: {
                            container: toolbarOptions,  // 工具栏
                            handlers: {
                                'image': function (value) {
                                    if (value) {
                                        // 触发input框选择图片文件
                                        document.querySelector('.avatar-uploader input').click()
                                    } else {
                                        this.quill.format('image', false);
                                    }
                                }
                            }
                        },
                        imagePaste: {}
                    }
                },
                uillUpdateImg: false
            }
        },
        mounted() {
        },
        components: {
            quillEditor
        },
        methods: {
            // 上传图片前
            beforeUpload(res, file) {
                // 显示loading动画
                this.quillUpdateImg = true
            },
            uploadSuccess(res, file) {
                // res为图片服务器返回的数据
                // 获取富文本组件实例
//                debugger
                let quill = this.$refs.myQuillEditor.quill
                // 如果上传成功
                if (res.result === true) {
//                if (res.code === '200' && res.info !== null) {
//                  debugger
                    // 获取光标所在位置
                    let length = quill.getSelection().index;
                    // 插入图片  res.info为服务器返回的图片地址
                    quill.insertEmbed(length, 'image', res.data)
                    // 调整光标到最后
                    quill.setSelection(length + 1)
                } else {
                    this.$message.error('图片插入失败')
                }
                // loading动画消失
                this.quillUpdateImg = false
            },

            // 富文本图片上传失败
            uploadError() {
                // loading动画消失
                this.quillUpdateImg = false;
                this.$message.error('图片插入失败')
            },
            onEditorChange({editor, html, text}) {
                this.content = text;
                this.$emit('docEditor', html);
                this.$emit('docEditorText', text)
            },
            submit() {
                console.log(this.content);
                this.$message.success('提交成功！');
            },
            onEditorReady() {

            }
        },
        watch: {
            docContent: function (val) {
                this.detailContent = this.docContent;
            }
        }
    }
</script>
<style scoped lang="less">
    .editor-btn {
        margin-top: 20px;
    }

    .doc-editor {
        .avatar-uploader {
            display: none;
        }
    }
</style>


<!--<template>-->
<!--<div>-->
<!--<quill-editor ref="myTextEditor" v-model="content" :options="editorOption"></quill-editor>-->
<!--</div>-->
<!--</template>-->

<!--<script>-->
<!--import 'quill/dist/quill.core.css';-->
<!--import 'quill/dist/quill.snow.css';-->
<!--import 'quill/dist/quill.bubble.css';-->
<!--import { quillEditor } from 'vue-quill-editor';-->


<!--// 工具栏配置-->
<!--const toolbarOptions = [-->
<!--['bold', 'italic','underline'],        // toggled buttons-->
<!--[{'header': 1}, {'header': 2}],-->
<!--[{'list': 'ordered'}, {'list': 'bullet'}],-->
<!--[{'size': ['small', false, 'large', 'huge']}],-->
<!--[{'header': [1, 2, 3, 4, 5, 6, false]}],-->
<!--['link', 'image']-->


<!--//        ['bold', 'italic', 'underline', 'strike'],        // toggled buttons-->
<!--//        ['blockquote', 'code-block'],-->
<!--//-->
<!--//        [{'header': 1}, {'header': 2}],               // custom button values-->
<!--//        [{'list': 'ordered'}, {'list': 'bullet'}],-->
<!--//        [{'script': 'sub'}, {'script': 'super'}],      // superscript/subscript-->
<!--//        [{'indent': '-1'}, {'indent': '+1'}],          // outdent/indent-->
<!--//        [{'direction': 'rtl'}],                         // text direction-->
<!--//-->
<!--//        [{'size': ['small', false, 'large', 'huge']}],  // custom dropdown-->
<!--//        [{'header': [1, 2, 3, 4, 5, 6, false]}],-->

<!--//        [{'color': []}, {'background': []}],          // dropdown with defaults from theme-->
<!--//        [{'font': []}],-->
<!--//        [{'align': []}],-->
<!--//        ['link', 'image', 'video'],-->
<!--//        ['clean']                                         // remove formatting button-->
<!--]-->
<!--export default {-->
<!--name: 'editor',-->
<!--data: function(){-->
<!--return {-->
<!--content: '',-->
<!--editorOption: {-->
<!--placeholder: 'Hello World',-->
<!--modules: {-->
<!--toolbar: {-->
<!--container: toolbarOptions,  // 工具栏-->
<!--handlers: {-->
<!--'image': function (value) {-->
<!--if (value) {-->
<!--alert(1)-->
<!--} else {-->
<!--this.quill.format('image', false);-->
<!--}-->
<!--}-->
<!--}-->
<!--}-->
<!--}-->
<!--}-->
<!--}-->
<!--},-->
<!--components: {-->
<!--quillEditor-->
<!--},-->
<!--methods: {-->
<!--onEditorChange({ editor, html, text }) {-->
<!--this.content = html;-->
<!--},-->
<!--submit(){-->
<!--console.log(this.content);-->
<!--this.$message.success('提交成功！');-->
<!--}-->
<!--}-->
<!--}-->
<!--</script>-->
<!--<style scoped>-->
<!--.editor-btn{-->
<!--margin-top: 20px;-->
<!--}-->
<!--</style>-->

































